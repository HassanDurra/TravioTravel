﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravioHotel.DataContext;

#nullable disable

namespace TravioHotel.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231105095435_UpdateFlightAndBooking")]
    partial class UpdateFlightAndBooking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravioHotel.Models.Aircraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("agent_id")
                        .HasColumnType("int");

                    b.Property<string>("aircraft_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aircraft_model_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aircraft_model_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("availibility")
                        .HasColumnType("int");

                    b.Property<int>("bussiness_seats")
                        .HasColumnType("int");

                    b.Property<int>("bussiness_seats_occupied")
                        .HasColumnType("int");

                    b.Property<int>("bussiness_seats_remaining")
                        .HasColumnType("int");

                    b.Property<int>("economy_seats")
                        .HasColumnType("int");

                    b.Property<int>("economy_seats_occupied")
                        .HasColumnType("int");

                    b.Property<int>("economy_seats_remaining")
                        .HasColumnType("int");

                    b.Property<int>("first_class_seats")
                        .HasColumnType("int");

                    b.Property<int>("first_class_seats_occupied")
                        .HasColumnType("int");

                    b.Property<int>("first_class_seats_remaining")
                        .HasColumnType("int");

                    b.Property<int>("remaining_seats")
                        .HasColumnType("int");

                    b.Property<int>("total_seats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Aircrafts");
                });

            modelBuilder.Entity("TravioHotel.Models.Airlines", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AirlineImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Airlinename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IATACode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ICAOCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deleted_at")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("TravioHotel.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country_iso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IataCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IcaoCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("delete_at")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("TravioHotel.Models.BookingFlightClientDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Cnic_number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contact_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_at")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("date_of_birth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("flight_details_id")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("is_booked")
                        .HasColumnType("int");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passport_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("BookingClientDetails");
                });

            modelBuilder.Entity("TravioHotel.Models.BookingFlightDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("air_craft_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("airline_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("airline_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arrival_date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arrival_time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("class_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_at")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deleted_at")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("departure_date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("departure_time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("flight_duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("from")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("journey_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("to")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("total_price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("BookingFlightDetails");
                });

            modelBuilder.Entity("TravioHotel.Models.BookingRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("created_at")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deleted_at")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("departure_date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("from_city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("from_country")
                        .HasColumnType("int");

                    b.Property<int>("number_of_adults")
                        .HasColumnType("int");

                    b.Property<string>("to_city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("to_country")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BookingRequests");
                });

            modelBuilder.Entity("TravioHotel.Models.Cities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("country_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("country_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("state_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("TravioHotel.Models.Countries", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("capital")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency_symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("emoji")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("iso2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("iso3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("native")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phonecode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subregion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("timezone")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tld")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("TravioHotel.Models.Service_account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("serviceId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Service_Account");
                });

            modelBuilder.Entity("TravioHotel.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("country_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("country_id")
                        .HasColumnType("int");

                    b.Property<string>("fips")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("iso2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("State");
                });

            modelBuilder.Entity("TravioHotel.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("User_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_at")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email_verified_at")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TravioHotel.Models.VerificationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Verification_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Verification_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Verification");
                });
#pragma warning restore 612, 618
        }
    }
}