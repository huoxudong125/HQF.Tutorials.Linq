using System;
using System.Collections.Generic;
using System.Linq;
using HQF.Tutorials.Linq.Models;
using Xunit;
using Xunit.Abstractions;

namespace HQF.Tutorials.Linq.XUnitTest
{
    public partial class LinqGroupUnitTest : IClassFixture<LinqStudentTestFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly LinqStudentTestFixture _testFixture;

        public LinqGroupUnitTest(ITestOutputHelper outputHelper,
            LinqStudentTestFixture testFixture)
        {
            _outputHelper = outputHelper;
            _testFixture = testFixture;
        }

        [Fact]
        public void TestGroupMethod()
        {
            _outputHelper.WriteLine("LinqGroup:");
            // Create the data source.
            List<Student> students = _testFixture.GetStudents();

            // Create the query.
            IEnumerable<Student> sortedStudents =
                from student in students
                orderby student.Last ascending, student.First ascending
                select student;

            // Execute the query.
            _outputHelper.WriteLine("sortedStudents:");
            foreach (Student student in sortedStudents)
                _outputHelper.WriteLine(student.Last + " " + student.First);

            // Now create groups and sort the groups. The query first sorts the names
            // of all students so that they will be in alphabetical order after they are
            // grouped. The second orderby sorts the group keys in alpha order.
            var sortedGroups =
                from student in students
                orderby student.Last, student.First
                group student by student.Last[0] into newGroup
                orderby newGroup.Key
                select newGroup;

            // Execute the query.
            _outputHelper.WriteLine(Environment.NewLine + "sortedGroups:");

            foreach (var studentGroup in sortedGroups)
            {
                _outputHelper.WriteLine(studentGroup.Key.ToString());

                foreach (var student in studentGroup)
                {
                    _outputHelper.WriteLine("   {0}, {1}", student.Last, student.First);
                }
            }

        }
    }
}