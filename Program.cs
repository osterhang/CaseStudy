using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyGamzeOsterhan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the locations: ");
            string consoleText = Console.ReadLine();
            string trimmedConsoleText = consoleText.Trim();
            string[] splittedText = trimmedConsoleText.Split(' ');
            List<int> values = splittedText.Select(int.Parse).ToList();
            
            string[] initialLocations = Console.ReadLine().Trim().Split(' ');
            Location location = new Location();
            location.XPosition = Convert.ToInt32(initialLocations[0]);
            location.YPosition = Convert.ToInt32(initialLocations[1]);
            location.Course = (Courses)Enum.Parse(typeof(Courses), initialLocations[2]);
            var actions = Console.ReadLine().ToUpper();
            try
            {
                location.InitialAction(values, actions);
                Console.WriteLine(String.Format("{0} {1} {2}", location.XPosition, location.YPosition, location.Course.ToString()));
                
            }
            catch (Exception exception){ Console.WriteLine(exception.Message); }

            Console.ReadLine();
        }

        public class Location : ILocation
        {
            public int XPosition { get; set; }
            public int YPosition { get; set; }
            public Courses Course { get; set; }

            public Location()
            {
                XPosition = 0;
                YPosition = 0;
                Course = Courses.North;
            }

            private void Turn90DegreesLeft()
            {
                if (this.Course == Courses.North) { this.Course = Courses.West; }
                else if (this.Course == Courses.South) { this.Course = Courses.East; }
                else if (this.Course == Courses.East) { this.Course = Courses.North; }
                else if (this.Course == Courses.West) { this.Course = Courses.South; }

            }

            private void Turn90DegreesRight()
            {
                if (this.Course == Courses.North) { this.Course = Courses.East; }
                else if (this.Course == Courses.South) { this.Course = Courses.West; }
                else if (this.Course == Courses.East) { this.Course = Courses.South; }
                else if (this.Course == Courses.West) { this.Course = Courses.North; }

            }

            private void GoInSameCourse()
            {
                if (this.Course == Courses.North) { this.YPosition += 1; }
                else if (this.Course == Courses.South) { this.YPosition -= 1; }
                else if (this.Course == Courses.East) { this.XPosition += 1; }
                else if (this.Course == Courses.West) { this.XPosition -= 1; }

            }

            public void InitialAction(List<int> max, string actions)
            {
                foreach (var action in actions)
                {
                    switch (action)
                    {
                        case 'M':
                            this.GoInSameCourse();
                            break;
                        case 'L':
                            this.Turn90DegreesLeft();
                            break;
                        case 'R':
                            this.Turn90DegreesRight();
                            break;
                        default:
                            Console.WriteLine("Wrong Char" + action);
                            break;
                    }

                    bool checkOfPositionValues = this.XPosition < 0
                        || this.XPosition > max[0]
                        || this.YPosition < 0
                        || this.YPosition > max[1];

                    string errorText = "The position data you entered is incorrect.";
                    if (checkOfPositionValues) { throw new Exception(errorText); }
                }
            }
        }

        public interface ILocation
        {
            void InitialAction(List<int> max, string actions);
        }

        public enum Courses{ North = 1,South = 2, East = 3, West = 4}

        

        
    }
}
