﻿using CampSleepaway.Controller;
using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Menus
{
    public class EditCouncelorsMenu
    {
        public static void Menu()
        {
            Console.Clear();

            List<Councelor> councelors = CouncelorController.GetCouncelors();
            List<string> options = new();

            foreach (var councelor in councelors)
            {
                options.Add(councelor.GetFullName());
            }

            int selection = HelperMethods.ShowMenu("Which Councelor would you like to manage?", options);

            ManageCouncelorMenu(councelors[selection]);
        }

        public static void ManageCouncelorMenu(Councelor councelor)
        {
            int councelorId = councelor.CouncelorId;

            Console.Clear();
            string temp = "None";
            if (councelor.Cabin is not null)
            {
                temp = councelor.Cabin.Name;
            }

            string title = $"{councelor.FirstName} {councelor.LastName}\n" +
                $"Phone Number: {councelor.PhoneNumber}\n" +
                $"Start Date: {councelor.StartDate:yyyy-MM-dd}\n" +
                $"End Date: {councelor.EndDate:yyyy-MM-dd}\n" +
                $"Favorite Number: {councelor.FavoriteNumber}" +
                $"Cabin: {temp}\n";

            int selection = HelperMethods.ShowMenu(title, new[]
            {
                "Modify Phone Number",
                "Modify Start Date",
                "Modify End Date",
                "Move Councelor to another Camp",
                "Delete Councelor"
            });

            switch (selection)
            {
                case 0:
                    string newPhoneNumber = PersonController.ProccessPhoneNumber();
                    CouncelorController.UpdatePhoneNumber(councelorId, newPhoneNumber);
                    break;
                case 1:
                    DateTime newStartDate = PersonController.ProccessStartDate();
                    CouncelorController.UpdateCouncelorStartDate(councelorId, newStartDate);
                    break;
                case 2:
                    DateTime newEndDate = PersonController.ProccessEndDate();
                    CouncelorController.UpdateCouncelorEndDate(councelorId,newEndDate);
                    break;
                case 3:
                    CouncelorController.UpdateFavoriteNumber(councelorId);
                    break;
                case 4:
                    break;
                case 5:
                    CouncelorController.DeleteCouncelor(councelorId);
                    break;
                default:
                    break;
            }
        }
    }
}
