using System;
using System.Collections.Generic;
using HQF.Tutorials.Linq.Models;

namespace HQF.Tutorials.Linq.XUnitTest.Fixtures
{
    public class LinqProductTestFixture : IDisposable
    {
        public LinqProductTestFixture()
        {
            Categories = new List<Category>()
        {
            new Category(){Name="Beverages", ID=001},
            new Category(){ Name="Condiments", ID=002},
            new Category(){ Name="Vegetables", ID=003},
            new Category() {  Name="Grains", ID=004},
            new Category() {  Name="Fruit", ID=005}
        };

            // Specify the second data source.
            Products = new List<Product>()
       {
          new Product{Name="Cola",  CategoryID=001},
          new Product{Name="Tea",  CategoryID=001},
          new Product{Name="Mustard", CategoryID=002},
          new Product{Name="Pickles", CategoryID=002},
          new Product{Name="Carrots", CategoryID=003},
          new Product{Name="Bok Choy", CategoryID=003},
          new Product{Name="Peaches", CategoryID=005},
          new Product{Name="Melons", CategoryID=005},
        };
        }

        // Specify the first data source.

        public List<Category> Categories { get; }
        public List<Product> Products { get; }

        public void Dispose()
        {
        }
    }
}