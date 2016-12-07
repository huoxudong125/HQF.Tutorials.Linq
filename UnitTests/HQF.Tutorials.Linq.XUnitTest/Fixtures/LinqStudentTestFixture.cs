using System;
using System.Collections.Generic;
using HQF.Tutorials.Linq.Models;

namespace HQF.Tutorials.Linq.XUnitTest
{
    public class LinqStudentTestFixture : IDisposable
    {
        public  List<Student> GetStudents()
        {
            // Use a collection initializer to create the data source. Note that each element
            //  in the list contains an inner sequence of scores.
            List<Student> students = new List<Student>
            {
               new Student {First="Svetlana", Last="Omelchenko", ID=111},
               new Student {First="Claire", Last="O'Donnell", ID=112},
               new Student {First="Sven", Last="Mortensen", ID=113},
               new Student {First="Cesar", Last="Garcia", ID=114},
               new Student {First="Debra", Last="Garcia", ID=115}
            };

            return students;
        }

        public void Dispose()
        {
           
        }
    }
}