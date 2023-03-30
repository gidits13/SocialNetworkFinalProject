using SocialNetworkFinalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkFinalProject.Data
{
    public class GenerateUsers
    {
        public readonly string[] maleNames = new string[] { "Александр", "Борис", "Василий", "Игорь", "Даниил", "Сергей", "Евгений", "Алексей", "Геогрий", "Валентин" };
        public readonly string[] femaleNames = new string[] { "Анна", "Мария", "Станислава", "Елена" };
        public readonly string[] lastNames = new string[] { "Тестов", "Титов", "Потапов", "Джабаев", "Иванов" };
        public readonly string[] malePics = new string[]
            {
                "https://i.ibb.co/1GBvXm6/1.jpg",
                "https://i.ibb.co/F6Z2YCd/2.jpg",
                "https://i.ibb.co/4KmnCgH/3.jpg",
                "https://i.ibb.co/BzzHHs5/4.jpg",
                "https://i.ibb.co/wzX6TCZ/5.jpg",
                "https://i.ibb.co/spS2St6/6.jpg",
                "https://i.ibb.co/M2qkMrk/7.jpg",
                "https://i.ibb.co/QrMPsd9/8.jpg",
                "https://i.ibb.co/H24LsvK/9.jpg",
                "https://i.ibb.co/G2Q1bC6/10.jpg",
                "https://i.ibb.co/Lz0Mx2v/11.jpg",
                "https://i.ibb.co/Nmt9m7k/12.jpg",
                "https://i.ibb.co/HNZWcs2/13.jpg",
                "https://i.ibb.co/drsc6TC/14.jpg",
                "https://i.ibb.co/809Xdvf/15.jpg",
                "https://i.ibb.co/WytzkYp/16.jpg",
                "https://i.ibb.co/9TFsHWV/17.jpg",
                "https://i.ibb.co/MkB38SN/18.jpg",
                "https://i.ibb.co/dp9gT8K/19.jpg",
                "https://i.ibb.co/G5Zm6NR/20.jpg"

            };
        public readonly string[] femalePics = new string[]
            {
                "https://i.ibb.co/Fgt5DPG/1.jpg",
                "https://i.ibb.co/CzMGNSd/2.jpg",
                "https://i.ibb.co/K06LSHZ/3.jpg",
                "https://i.ibb.co/qmcRw9b/4.jpg",
                "https://i.ibb.co/vQyXnmk/5.jpg",
                "https://i.ibb.co/WV75HMP/6.jpg",
                "https://i.ibb.co/8bp4Wbp/7.jpg",
                "https://i.ibb.co/sJFHLsh/8.jpg",
                "https://i.ibb.co/qBXLhxR/9.jpg",
                "https://i.ibb.co/mhKbkG9/10.jpg",
                "https://i.ibb.co/PYhQ9cM/11.jpg",
                "https://i.ibb.co/NnTZ3jB/12.jpg",
                "https://i.ibb.co/B4cbCVj/13.jpg",
                "https://i.ibb.co/VJtd8Pw/14.jpg",
                "https://i.ibb.co/3vPzfcv/15.jpg",
                "https://i.ibb.co/SJNGCX6/16.jpg",
                "https://i.ibb.co/D7qYS1m/17.jpg",
                "https://i.ibb.co/BTKHgHD/18.jpg",
                "https://i.ibb.co/ZxCM92X/19.jpg",
                "https://i.ibb.co/jvGPNMg/20.jpg"
            };
        public List<User> Populate(int c)
        {
            var rand = new Random();
            var users = new List<User>();
            for(int i = 0; i < c; i++)
            {
                string img;
                string firstName;
                

                var male = rand.Next(1, 3) == 1;
                var lastName = lastNames[rand.Next(0, lastNames.Length)];
                if(male)
                {
                    firstName = maleNames[rand.Next(0, maleNames.Length)];
                    img = malePics[rand.Next(0, malePics.Length)];
                }
                else
                {
                    firstName = femaleNames[rand.Next(0, femaleNames.Length)];
                    lastName = lastName + "a";
                    img = femalePics[rand.Next(0, femalePics.Length)];
                }
                var user = new User()
                {
                    FirstName=firstName,
                    LastName=lastName,
                    BirthDate = DateTime.Now.AddYears(-rand.Next(7,100)),
                    Email = "test"+rand.Next(0,1024)+"test.com"                    
                };
                user.UserName = user.Email;
                user.Image = img;
                users.Add(user);
            }
            return users;
        }
    }
}
