using System;
using System.Text;

namespace Utils.Hash
{
    public static class GeoTool
    {
        public static string Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"First position => lat1: {lat1}, lon1: {lon1} ");
            builder.AppendLine($"Second position => lat2: {lat2}, lon2: {lon2} ");
            var theta = lon1 - lon2;
            builder.AppendLine($"theta = {theta}");
            var dist = Math.Sin(Rad(lat1)) * Math.Sin(Rad(lat2)) + Math.Cos(Rad(lat1)) * Math.Cos(Rad(lat2)) * Math.Cos(Rad(theta));
            builder.AppendLine($"dist1 = {dist}");
            dist = Math.Acos(dist);
            builder.AppendLine($"dist2 = {dist}");
            dist = Deg(dist);
            builder.AppendLine($"dist3 = {dist}");
            dist = dist * 60 * 1.1515;
            builder.AppendLine($"dist4 = {dist}");
            switch (unit)
            {
                case 'K':
                    dist = dist * 1.609344;
                    builder.AppendLine($"distK = {dist}");
                    break;
                case 'N':
                    dist = dist * 0.8684;
                    builder.AppendLine($"distN = {dist}");
                    break;
                default:
                    dist = dist * 1.609344;
                    builder.AppendLine($"distD = {dist}");
                    break;
            }
            //return (dist);
            return builder.ToString();
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
