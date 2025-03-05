﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonRegistry.Persistence.Context;

#nullable disable

namespace PersonRegistry.Persistance.Migrations
{
    [DbContext(typeof(PersonRegistryDbContext))]
    partial class PersonRegistryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.City.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Cities", "City");
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.Person.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .IsFixedLength();

                    b.Property<string>("Photo")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Persons", "Person", t =>
                        {
                            t.HasCheckConstraint("CK_Person_BirthDate_MinLength", "DATEDIFF(hour, BirthDate, GETDATE()) / 8766.0 >= 18");

                            t.HasCheckConstraint("CK_Person_LastName_MinLength", "LEN(LastName) >= 2");

                            t.HasCheckConstraint("CK_Person_Name_MinLength", "LEN(Name) >= 2");
                        });
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.Person.PersonPhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PhoneNumberTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PhoneNumberTypeId");

                    b.ToTable("PersonPhoneNumbers", "Person", t =>
                        {
                            t.HasCheckConstraint("CK_PersonPhoneNumber_PhoneNumber_MinLength", "LEN(PhoneNumber) >= 4");
                        });
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.Person.PersonRelation.PersonRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("PersonRelationTypeId")
                        .HasColumnType("int");

                    b.Property<int>("RelatedPersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PersonRelationTypeId");

                    b.HasIndex("RelatedPersonId");

                    b.ToTable("PersonRelations", "Person");
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.PersonRelationType.PersonRelationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("PersonRelationTypes", "Person");
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.PhoneNumberType.PhoneNumberType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PhoneNumberTypes", "Person");
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.Person.Person", b =>
                {
                    b.HasOne("PersonRegistry.Domain.Aggregates.City.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.Person.PersonPhoneNumber", b =>
                {
                    b.HasOne("PersonRegistry.Domain.Aggregates.Person.Person", "Person")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonRegistry.Domain.Aggregates.PhoneNumberType.PhoneNumberType", "PhoneNumberType")
                        .WithMany()
                        .HasForeignKey("PhoneNumberTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("PhoneNumberType");
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.Person.PersonRelation.PersonRelation", b =>
                {
                    b.HasOne("PersonRegistry.Domain.Aggregates.Person.Person", "Person")
                        .WithMany("RelatedPersons")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PersonRegistry.Domain.Aggregates.PersonRelationType.PersonRelationType", "PersonRelationType")
                        .WithMany()
                        .HasForeignKey("PersonRelationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonRegistry.Domain.Aggregates.Person.Person", "RelatedPerson")
                        .WithMany()
                        .HasForeignKey("RelatedPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("PersonRelationType");

                    b.Navigation("RelatedPerson");
                });

            modelBuilder.Entity("PersonRegistry.Domain.Aggregates.Person.Person", b =>
                {
                    b.Navigation("PhoneNumbers");

                    b.Navigation("RelatedPersons");
                });
#pragma warning restore 612, 618
        }
    }
}
