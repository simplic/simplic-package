using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Package.Configuration
{
    /// <summary>
    /// Request value service implementation for test modes.
    /// </summary>
    public class TestRequestConfigurationValueService : IRequestValueService
    {
        private Random random = new Random();

        /// <summary>
        /// Requests the value for all configurations that need a requested value.
        /// </summary>
        /// <param name="installableObjects">A list of installable objects with cofiguration objects as content.</param>
        /// <returns>Wether the installation was successfull.</returns>
        public RequestValueResult RequestValue(IList<InstallableObject> installableObjects)
        {

            foreach (var installableObject in installableObjects)
            {
                if (installableObject.Content is Configuration configuration)
                {
                    if (configuration.ValueSource == ConfigurationValueSource.RequestValue)
                    {
                        configuration.Value = RandomString(20);
                    }
                }
            }

            return new RequestValueResult { Success = true };
        }

        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <param name="length">Length of the string.</param>
        /// <returns>The string.</returns>
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
