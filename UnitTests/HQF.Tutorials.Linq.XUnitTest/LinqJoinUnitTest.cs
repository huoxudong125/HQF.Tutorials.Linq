using System;
using System.Collections.Generic;
using System.Linq;
using HQF.Tutorials.Linq.Models;
using HQF.Tutorials.Linq.XUnitTest.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace HQF.Tutorials.Linq.XUnitTest
{
    public class LinqJoinUnitTest:IClassFixture<LinqProductTestFixture>
    {
        private readonly ITestOutputHelper _outputHelper;

        private readonly List<Category> categories;
        private readonly List<Product> products;
        public LinqJoinUnitTest(LinqProductTestFixture testFixture,ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            categories = testFixture.Categories;
            products = testFixture.Products;
        }

        [Fact]
        void InnerJoin()
        {
            // Create the query that selects 
            // a property from each element.
            var innerJoinQuery =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID
               select new { Category = category.ID, Product = prod.Name };

            _outputHelper.WriteLine("InnerJoin:");
            // Execute the query. Access results 
            // with a simple foreach statement.
            foreach (var item in innerJoinQuery)
            {
                _outputHelper.WriteLine("{0,-10}{1}", item.Product, item.Category);
            }
            _outputHelper.WriteLine("InnerJoin: {0} items in 1 group.", innerJoinQuery.Count());
            _outputHelper.WriteLine(System.Environment.NewLine);

        }
        [Fact]
        void GroupJoin()
        {
            // This is a demonstration query to show the output
            // of a "raw" group join. A more typical group join
            // is shown in the GroupInnerJoin method.
            var groupJoinQuery =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID into prodGroup
               select prodGroup;

            // Store the count of total items (for demonstration only).
            int totalItems = 0;

            _outputHelper.WriteLine("Simple GroupJoin:");

            // A nested foreach statement is required to access group items.
            foreach (var prodGrouping in groupJoinQuery)
            {
                _outputHelper.WriteLine("Group:");
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    _outputHelper.WriteLine("   {0,-10}{1}", item.Name, item.CategoryID);
                }
            }
            _outputHelper.WriteLine("Unshaped GroupJoin: {0} items in {1} unnamed groups", totalItems, groupJoinQuery.Count());
            _outputHelper.WriteLine(System.Environment.NewLine);
        }
        [Fact]
        void GroupInnerJoin()
        {
            var groupJoinQuery2 =
                from category in categories
                orderby category.ID
                join prod in products on category.ID equals prod.CategoryID into prodGroup
                select new
                {
                    Category = category.Name,
                    Products = from prod2 in prodGroup
                               orderby prod2.Name
                               select prod2
                };

            //_outputHelper.WriteLine("GroupInnerJoin:");
            int totalItems = 0;

            _outputHelper.WriteLine("GroupInnerJoin:");
            foreach (var productGroup in groupJoinQuery2)
            {
                _outputHelper.WriteLine(productGroup.Category);
                foreach (var prodItem in productGroup.Products)
                {
                    totalItems++;
                    _outputHelper.WriteLine("  {0,-10} {1}", prodItem.Name, prodItem.CategoryID);
                }
            }
            _outputHelper.WriteLine("GroupInnerJoin: {0} items in {1} named groups", totalItems, groupJoinQuery2.Count());
            _outputHelper.WriteLine(System.Environment.NewLine);
        }
        [Fact]
        void GroupJoin3()
        {

            var groupJoinQuery3 =
                from category in categories
                join product in products on category.ID equals product.CategoryID into prodGroup
                from prod in prodGroup
                orderby prod.CategoryID
                select new { Category = prod.CategoryID, ProductName = prod.Name };

            //_outputHelper.WriteLine("GroupInnerJoin:");
            int totalItems = 0;

            _outputHelper.WriteLine("GroupJoin3:");
            foreach (var item in groupJoinQuery3)
            {
                totalItems++;
                _outputHelper.WriteLine("   {0}:{1}", item.ProductName, item.Category);
            }

            _outputHelper.WriteLine("GroupJoin3: {0} items in 1 group", totalItems, groupJoinQuery3.Count());
            _outputHelper.WriteLine(System.Environment.NewLine);
        }

        [Fact]
        void LeftOuterJoin()
        {
            // Create the query.
            var leftOuterQuery =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID into prodGroup
               select prodGroup.DefaultIfEmpty(new Product() { Name = "Nothing!", CategoryID = category.ID });

            // Store the count of total items (for demonstration only).
            int totalItems = 0;

            _outputHelper.WriteLine("Left Outer Join:");

            // A nested foreach statement  is required to access group items
            foreach (var prodGrouping in leftOuterQuery)
            {
                _outputHelper.WriteLine("Group:", prodGrouping.Count());
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    _outputHelper.WriteLine("  {0,-10}{1}", item.Name, item.CategoryID);
                }
            }
            _outputHelper.WriteLine("LeftOuterJoin: {0} items in {1} groups", totalItems, leftOuterQuery.Count());
            _outputHelper.WriteLine(System.Environment.NewLine);
        }

        [Fact]
        public void LeftOuterJoin2()
        {
            // Create the query.
            var leftOuterQuery2 =
               from category in categories
               join prod in products on category.ID equals prod.CategoryID into prodGroup
               from item in prodGroup.DefaultIfEmpty()
               select new { Name = item == null ? "Nothing!" : item.Name, CategoryID = category.ID };

            _outputHelper.WriteLine("LeftOuterJoin2: {0} items in 1 group", leftOuterQuery2.Count());
            // Store the count of total items
            int totalItems = 0;

            _outputHelper.WriteLine("Left Outer Join 2:");

            // Groups have been flattened.
            foreach (var item in leftOuterQuery2)
            {
                totalItems++;
                _outputHelper.WriteLine("{0,-10}{1}", item.Name, item.CategoryID);
            }
            _outputHelper.WriteLine("LeftOuterJoin2: {0} items in 1 group", totalItems);
        }
    }
}

