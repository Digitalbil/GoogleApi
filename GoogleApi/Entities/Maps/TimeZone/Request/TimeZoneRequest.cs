﻿using System;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Helpers;

namespace GoogleApi.Entities.Maps.TimeZone.Request
{
    /// <summary>
    /// The Google Maps Time Zone API provides time offset data for locations on the surface of the earth. 
    /// Requesting the time zone information for a specific Latitude/Longitude pair will return the name of that time zone, the time offset from UTC, and the Daylight Savings offset
    /// </summary>
    public class TimeZoneRequest : SignableRequest
    {
        private const string BASE_URL = "maps.googleapis.com/maps/api/timezone/";

        /// <summary>
        /// A comma-separated lat,lng tuple (eg. location=-33.86,151.20), representing the location to look up
        /// </summary>
        public virtual Location Location { get; set; } 
        
        /// <summary>
        /// Timestamp specifies the desired time as seconds since midnight, January 1, 1970 UTC. The Time Zone API uses the timestamp to determine whether or not Daylight Savings should be applied. Times before 1970 can be expressed as negative values.
        /// </summary>
        public virtual DateTime TimeStamp { get; set; }
        
        /// <summary>
        /// The language in which to return results. See the list of supported domain languages. Note that we often update supported languages so this list may not be exhaustive. Defaults to en.
        /// </summary>
        public virtual string Language { get; set; }

        /// <summary>
        /// Always true. Setter is not supported.
        /// </summary>
        public override bool IsSsl
        {
            get { return true; }
            set { throw new NotSupportedException("This operation is not supported, TimeZoneRequest must use SSL"); }
        }

        protected internal override string BaseUrl
        {
            get { return TimeZoneRequest.BASE_URL; }
        }

        protected override QueryStringParametersList GetQueryStringParameters()
        {
            if (this.Location == null)
                throw new ArgumentException("Location is required");

            if (this.TimeStamp == null)
                throw new ArgumentException("TimeStamp is required");

            var _parameters = base.GetQueryStringParameters();

            _parameters.Add("location", this.Location.LocationString);
            _parameters.Add("timestamp", UnixTimeConverter.DateTimeToUnixTimestamp(this.TimeStamp).ToString());

            if (!string.IsNullOrWhiteSpace(this.Language)) 
                _parameters.Add("language", this.Language);

            return _parameters;
        }
    }
}