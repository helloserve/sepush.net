using helloserve.SePush.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.SePush
{
    public interface ISePush
    {
        /// <summary>
        /// The current and next loadshedding statuses for South Africa and (Optional) municipal overrides
        /// "eskom" is the National status
        /// Other keys in the status refer to different municipalities and potential overrides from the National status; most typically present is the key for "capetown"
        /// </summary>
        /// <returns>A <see cref="Status"/>object</returns>
        Task<Status> StatusAsync();

        /// <summary>
        /// This single request has everything you need to monitor upcoming loadshedding events for the chosen suburb.
        /// </summary>
        /// <param name="id">The id of the area. Obtain the id from Nearby Area or Area Search and use with this request.</param>
        /// <param name="testMode">Optional parameter for testing. It does not count towards your allowace. Use "current" or "future" for specific results.</param>
        /// <returns>A complete <see cref="AreaInformation"/> object</returns>
        Task<AreaInformation> AreaInformationAsync(string id, string testMode = null);

        /// <summary>
        /// Find areas based on GPS coordinates (latitude and longitude). These are recommended areas based on EskomSePush users adding locations nearby to those coordinates.
        /// The first area returned is typically the best choice for the coordinates - as it's the most popular used.
        /// </summary>
        /// <param name="latitude">The latitudinal portion of the GPS coordinate as a double number</param>
        /// <param name="longitude">The longitudinal portion of the GPS coordinate as a double number</param>
        /// <returns>A collection of nearby areas.</returns>
        Task<IEnumerable<AreaNearby>> AreasNearbyAsync(double latitude, double longitude);

        /// <summary>
        /// Search area based on text
        /// </summary>
        /// <param name="text">The search criteria to use</param>
        /// <returns>A collection areas</returns>
        Task<IEnumerable<Area>> AreasSearchAsync(string text);
        
        /// <summary>
        /// Find topics created by users based on GPS coordinates (latitude and longitude). Can use this to detect if there is a potential outage/problem nearby.
        /// </summary>
        /// <param name="latitude">The latitudinal portion of the GPS coordinate as a double number</param>
        /// <param name="longitude">The longitudinal portion of the GPS coordinate as a double number</param>
        /// <returns>A collection of topics</returns>
        Task<IEnumerable<Topic>> TopicsNearbyAsync(double latitude, double longitude);

        /// <summary>
        /// Check allowance allocated for token
        /// NOTE: This call doesn't count towards your quota.
        /// </summary>
        /// <returns>An <see cref="Allowance"/> object</returns>
        Task<Allowance> CheckAllowanceAsync();
    }
}
