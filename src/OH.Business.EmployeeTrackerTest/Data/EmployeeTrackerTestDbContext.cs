﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OH.Common.EmployeeTrackerTest.Models;

namespace OH.Business.EmployeeTrackerTest.Data
{
    public partial class EmployeeTrackerTestDbContext : DbContext
    {
        public EmployeeTrackerTestDbContext()
        {
        }

        public EmployeeTrackerTestDbContext(DbContextOptions<EmployeeTrackerTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WeatherForecast> WeatherForecast { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}