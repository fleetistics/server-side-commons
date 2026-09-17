namespace exs.Commons.Geo
{
    public struct GeoLocation
    {
        public GeoLocation(float lat, float lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
