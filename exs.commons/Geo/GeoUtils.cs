namespace exs.Commons.Geo
{
    public static class GeoUtils
    {
        public const double EarthRadiusKm = 6371;
        public const double KmPerDegree = 111.111;

        public static double ToRadians(double deg)
        {
            return (deg * Math.PI) / 180.0;
        }

        public static double MPH2KMH(double speed)
        {
            return speed * 1.60934;
        }

        public static double KMH2MPH(double speed)
        {
            return speed * 0.621371;
        }

        private const double Eps = 0.00001;
        public static bool EqualCoord(double c1, double c2)
        {
            return Math.Abs(c1 - c2) < Eps;
        }

        public static float KmToDegree(float km)
        {
            return (float)(km / KmPerDegree);
        }

        public static double DistanceToSegment(double x, double y, double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1, dy = y2 - y1;
            if (dx == 0 && dy == 0)
            {
                dx = x - x1;
                dy = y - y1;
            }
            else
            {
                double t = ((x - x1) * dx + (y - y1) * dy) / (dx * dx + dy * dy);
                if (t < 0)
                {
                    dx = x - x1;
                    dy = y - y1;
                }
                else if (t > 1)
                {
                    dx = x - x2;
                    dy = y - y2;
                }
                else
                {
                    dx = x - x1 - t * dx;
                    dy = y - y1 - t * dy;
                }
            }
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static float DistanceToSegment(float x, float y, float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1, dy = y2 - y1;
            if (dx == 0 && dy == 0)
            {
                dx = x - x1;
                dy = y - y1;
            }
            else
            {
                float t = ((x - x1) * dx + (y - y1) * dy) / (dx * dx + dy * dy);
                if (t < 0)
                {
                    dx = x - x1;
                    dy = y - y1;
                }
                else if (t > 1)
                {
                    dx = x - x2;
                    dy = y - y2;
                }
                else
                {
                    dx = x - x1 - t * dx;
                    dy = y - y1 - t * dy;
                }
            }
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static decimal DistanceToSegment(decimal x, decimal y, decimal x1, decimal y1, decimal x2, decimal y2)
        {
            decimal dx = x2 - x1, dy = y2 - y1;
            if (dx == 0 && dy == 0)
            {
                dx = x - x1;
                dy = y - y1;
            }
            else
            {
                decimal t = ((x - x1) * dx + (y - y1) * dy) / (dx * dx + dy * dy);
                if (t < 0)
                {
                    dx = x - x1;
                    dy = y - y1;
                }
                else if (t > 1)
                {
                    dx = x - x2;
                    dy = y - y2;
                }
                else
                {
                    dx = x - x1 - t * dx;
                    dy = y - y1 - t * dy;
                }
            }
            return (decimal)Math.Sqrt((double)(dx * dx + dy * dy));
        }

        public static short Bearing(float lat1, float lng1, float lat2, float lng2)
        {
            var y = Math.Sin(ToRadians(lng2 - lng1)) * Math.Cos(ToRadians(lat2));
            var x = Math.Cos(ToRadians(lat1)) * Math.Sin(ToRadians(lat2)) -
                Math.Sin(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Cos(ToRadians(lng2 - lng1));
            var brng = (Math.Atan2(y, x) * 180.0) / Math.PI;
            return (short)(((int)Math.Floor(brng + 0.5) + 360) % 360);
        }

        public static double DistanceKm(float lat1, float lng1, float lat2, float lng2)
        {
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lng2 - lng1);

            double s1 = Math.Sin(dLat / 2), s2 = Math.Sin(dLon / 2);
            double a = s1 * s1 + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) * s2 * s2;
            //return EarthRadiusKm * 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            return EarthRadiusKm * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        }

        public static double DistanceKm(GeoLocation l1, GeoLocation l2)
        {
            return DistanceKm(l1.Latitude, l1.Longitude, l2.Latitude, l2.Longitude);
        }

        public static ushort CalcSection(float lat, float lng)
        {
            int ilat = (int)(lat + 90f) / 2;
            int ilng = (int)(lng + 180f) / 2;
            return (ushort)(ilng * 100 + ilat);
        }

        public static ushort CalcSection(GeoLocation l)
        {
            return CalcSection(l.Latitude, l.Longitude);
        }

        public static ushort[] CalcSections(float swLat, float swLng, float neLat, float neLng)
        {
            int ilat1 = (int)(swLat + 90f) / 2, ilat2 = (int)(neLat + 90f) / 2;
            int ilng1 = (int)(swLng + 180f) / 2, ilng2 = (int)(neLng + 180f) / 2;
            if (ilat1 > ilat2 + 1)
            {
                var t = ilat1;
                ilat1 = ilat2;
                ilat2 = t;
            }
            if (ilng1 > ilng2 + 1)
            {
                var t = ilng1;
                ilng1 = ilng2;
                ilng2 = t;
            }
            ushort[] sections = new ushort[(ilng2 - ilng1 + 1)*(ilat2 - ilat1 + 1)];
            int i = 0;
            if (ilng1 > ilng2)
            {
                var t = ilng2;
                ilng2 = ilng1;
                ilng1 = t;
            }
            if (ilat1 > ilat2)
            {
                var t = ilat2;
                ilat2 = ilat1;
                ilat1 = t;
            }
            for (var ilng = ilng1; ilng <= ilng2; ilng++)
            {
                for (var ilat = ilat1; ilat <= ilat2; ilat++)
                {
                    sections[i++] = (ushort)(ilng * 100 + ilat);
                }
            }
            return sections;
        }

        public static bool IsPointInsideCircle(GeoLocation point, GeoLocation center, double radius)
        {
            return DistanceKm(point, center) <= radius;
        }

        public static bool IsPointInsidePolygon(GeoLocation point, List<GeoLocation> polygon)
        {
            var cnt = polygon.Count;
            if (cnt < 3)
            {
                return false;
            }
            var firstPt = polygon[0];
            var lastPt = polygon[cnt - 1];
            if (!EqualCoord(firstPt.Latitude, lastPt.Latitude) || !EqualCoord(firstPt.Longitude, lastPt.Longitude))
            {
                polygon.Add(firstPt);
                cnt++;
            }

            double isLeft(GeoLocation pt, GeoLocation pt1, GeoLocation pt2)
            {
                return (pt2.Latitude - pt1.Latitude) * (pt.Longitude - pt1.Longitude) -
                     (pt.Latitude - pt1.Latitude) * (pt2.Longitude - pt1.Longitude);
            }

            int wn = 0;
            for (int i = 0; i < cnt - 1; i++)
            {
                if (polygon[i].Longitude <= point.Longitude)
                {
                    if (polygon[i + 1].Longitude > point.Longitude)
                    {
                        if (isLeft(polygon[i], polygon[i + 1], point) > 0.0)
                        {
                            ++wn;
                        }
                    }
                }
                else
                {
                    if (polygon[i + 1].Longitude <= point.Longitude)
                    {
                        if (isLeft(polygon[i], polygon[i + 1], point) < 0.0)
                        {
                            --wn;
                        }
                    }
                }
            }

            return wn != 0;
        }
    }
}
