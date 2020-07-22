using Microsoft.AspNetCore.Http;
using System;

namespace DatingSiteBackend.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method for rewriting the application error message 
        /// to get rid of the cors error message incase of exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="message"></param>
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }


        /// <summary>
        /// Extension method to calcuate age (exteding the DateTime)
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            // check if the user birthday is gone already or not
            if (dateOfBirth.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;
        }
    }
}
