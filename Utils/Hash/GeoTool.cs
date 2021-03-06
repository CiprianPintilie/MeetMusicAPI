﻿using System;

namespace Utils.Hash
{
    public static class GeoTool
    {
        public static double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            var theta = lon1 - lon2;
            var dist = Math.Sin(Rad(lat1)) * Math.Sin(Rad(lat2)) + Math.Cos(Rad(lat1)) * Math.Cos(Rad(lat2)) * Math.Cos(Rad(theta));
            dist = Math.Acos(dist);
            dist = Deg(dist);
            dist = dist * 60 * 1.1515;
            switch (unit)
            {
                case 'K':
                    dist = dist * 1.609344;
                    break;
                case 'N':
                    dist = dist * 0.8684;
                    break;
                default:
                    dist = dist * 1.609344;
                    break;
            }
            return (dist);
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
