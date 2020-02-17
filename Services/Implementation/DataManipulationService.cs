using ChallengeApplication.Clients;
using ChallengeApplication.Models.DTO;
using ChallengeApplication.Models.Entities;
using ChallengeApplication.Models.Entities.Requests;
using ChallengeApplication.Models.Entities.Responses;
using ChallengeApplication.Models.Requests;
using ChallengeApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeApplication.Services.Implementation
{
    // Missing parts: error handler, logger 
    public class DataManipulationService : IDataManipulationService
    {
        private readonly IAdformClient _adformClient;

        public DataManipulationService(IAdformClient adformClient)
        {
            _adformClient = adformClient;
        }

        public async Task<ImpressionsDTO> AggregateImpressionsDataAsync()
        {
            var request = new ReportDataRequest()
            {
                Filter = new Filter()
                {
                    Date = "LastTwoYears"
                },
                Dimensions = new List<string>() { "date" },
                Metrics = new List<string>() { "impressions" }

            };
            var response = await _adformClient.CallClient<ReportDataResponse>(HttpMethod.Post, "reportingstats/publisher/reportdata", request);
            var impressions = GetImpressions(response.response.ReportData);
            return new ImpressionsDTO() { ImpressionDetails = impressions };

        }

        public async Task<AnomaliesDatesDTO> FindAnomaliesDatesAsync()
        {
            var request = new ReportDataRequest()
            {
                Filter = new Filter()
                {
                    Date = "LastTwoYears"
                },
                Dimensions = new List<string>() { "date" },
                Metrics = new List<string>() { "bidRequests" }

            };
            var response = await _adformClient.CallClient<ReportDataResponse>(HttpMethod.Post, "reportingstats/publisher/reportdata", request);
            var datesList = GetAnomaliesDates(response.response.ReportData);
            return new AnomaliesDatesDTO() { Dates = datesList.ToArray() };
        }
      

        private List<ImpressionDetailsDTO> GetImpressions(ReportData reportData)
        {
            var currentCulture = CultureInfo.InvariantCulture;
            var impressions = new List<ImpressionDetailsDTO>();

            for (int i = 0; i < reportData.Rows.GetLength(0); i++)
            {
                var weekId = currentCulture.Calendar.GetWeekOfYear(DateTime.Parse(reportData.Rows[i, 0].ToString()), currentCulture.DateTimeFormat.CalendarWeekRule,
                                      currentCulture.DateTimeFormat.FirstDayOfWeek);
                var count = (long)reportData.Rows[i, 1];
                if (impressions.Any(x => x.WeekId == weekId))
                {
                    var impressionItem = impressions.Where(x => x.WeekId == weekId).FirstOrDefault();
                    impressionItem.ImpressionsCount += count;
                }
                else
                {
                    impressions.Add(new ImpressionDetailsDTO()
                    {
                        WeekId = weekId,
                        ImpressionsCount = count
                    });
                }
            }

            return impressions;
        }
        private List<DateTime> GetAnomaliesDates(ReportData reportData)
        {
            List<DateTime> datesList = new List<DateTime>();
            for (int i = 0; i < reportData.Rows.GetLength(0); i++)
            {
                if (i == 0) continue;

                var bidRequests = (long)reportData.Rows[i, 1];
                if (Math.Max(bidRequests, (long)reportData.Rows[i - 1, 1]) / Math.Min(bidRequests, (long)reportData.Rows[i - 1, 1]) >= 3)
                    datesList.Add(DateTime.Parse(reportData.Rows[i, 0].ToString()));
            }
            return datesList;
        }
    }
}
