﻿// <auto-generated />
using System;
using Adventour.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lucrare_de_licenta.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250525071127_Mg1")]
    partial class Mg1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lucrare_de_licenta.Models.Beneficiar", b =>
                {
                    b.Property<int>("cod_beneficiar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_beneficiar");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_beneficiar"));

                    b.Property<int>("cod_camera")
                        .HasColumnType("int")
                        .HasColumnName("cod_camera");

                    b.Property<DateOnly>("data_nastere")
                        .HasColumnType("date")
                        .HasColumnName("data_nastere");

                    b.Property<string>("nume_beneficiar")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nume_beneficiar");

                    b.Property<string>("prenume_beneficiar")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("prenume_beneficiar");

                    b.HasKey("cod_beneficiar");

                    b.HasIndex("cod_camera");

                    b.ToTable("beneficiari");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Camera", b =>
                {
                    b.Property<int>("cod_camera")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_camera");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_camera"));

                    b.Property<int>("cod_rezervare")
                        .HasColumnType("int")
                        .HasColumnName("cod_rezervare");

                    b.HasKey("cod_camera");

                    b.HasIndex("cod_rezervare");

                    b.ToTable("Camere");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Categorie", b =>
                {
                    b.Property<byte>("cod_categ")
                        .HasColumnType("tinyint")
                        .HasColumnName("cod_categ");

                    b.Property<string>("den_categ")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("den_categ");

                    b.Property<string>("desc_categ")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("desc_categ");

                    b.Property<string>("img_categ")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("img_categ");

                    b.HasKey("cod_categ");

                    b.ToTable("categorii");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Cazare", b =>
                {
                    b.Property<int>("cod_cazare")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_cazare");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_cazare"));

                    b.Property<string>("adresa_cazare")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("adresa_cazare");

                    b.Property<TimeSpan?>("check_in_final")
                        .HasColumnType("time")
                        .HasColumnName("check_in_final");

                    b.Property<TimeSpan>("check_in_inceput")
                        .HasColumnType("time")
                        .HasColumnName("check_in_inceput");

                    b.Property<TimeSpan>("check_out")
                        .HasColumnType("time")
                        .HasColumnName("check_out");

                    b.Property<short>("cod_destinatie")
                        .HasColumnType("smallint")
                        .HasColumnName("cod_destinatie");

                    b.Property<string>("den_cazare")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("den_cazare");

                    b.Property<string>("desc_cazare")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("desc_cazare");

                    b.Property<byte>("nr_stele")
                        .HasColumnType("tinyint")
                        .HasColumnName("nr_stele");

                    b.Property<byte>("tip_cazare")
                        .HasColumnType("tinyint")
                        .HasColumnName("tip_cazare");

                    b.HasKey("cod_cazare");

                    b.HasIndex("cod_destinatie");

                    b.ToTable("cazari");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Destinatie", b =>
                {
                    b.Property<short>("cod_destinatie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("cod_destinatie");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("cod_destinatie"));

                    b.Property<byte>("cod_tara")
                        .HasColumnType("tinyint")
                        .HasColumnName("cod_tara");

                    b.Property<string>("den_destinatie")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("den_destinatie");

                    b.Property<string>("desc_destinatie")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("desc_destinatie");

                    b.Property<string>("img_destinatie")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("img_destinatie");

                    b.Property<string>("judet")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("judet");

                    b.HasKey("cod_destinatie");

                    b.HasIndex("cod_tara");

                    b.ToTable("destinatii");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Destinatie_itinerariu", b =>
                {
                    b.Property<int>("cod_dest_itin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_dest_itin");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_dest_itin"));

                    b.Property<short>("cod_destinatie")
                        .HasColumnType("smallint")
                        .HasColumnName("cod_destinatie");

                    b.Property<int>("cod_itinerariu")
                        .HasColumnType("int")
                        .HasColumnName("cod_itinerariu");

                    b.HasKey("cod_dest_itin");

                    b.HasIndex("cod_destinatie");

                    b.HasIndex("cod_itinerariu");

                    b.ToTable("destinatii_itinerarii");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Factura", b =>
                {
                    b.Property<int>("cod_factura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_factura");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_factura"));

                    b.Property<int>("cod_plata")
                        .HasColumnType("int")
                        .HasColumnName("cod_plata");

                    b.Property<DateOnly>("data_achitarii")
                        .HasColumnType("date")
                        .HasColumnName("data_achitarii");

                    b.Property<byte?>("nr_rata")
                        .HasColumnType("tinyint")
                        .HasColumnName("nr_rata");

                    b.Property<byte>("status_factura")
                        .HasColumnType("tinyint")
                        .HasColumnName("status_factura");

                    b.Property<string>("stripe_charge_id")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("stripe_charge_id");

                    b.Property<string>("stripe_currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("stripe_currency");

                    b.Property<decimal>("suma_factura")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("suma_factura");

                    b.HasKey("cod_factura");

                    b.HasIndex("cod_plata");

                    b.ToTable("facturi");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Itinerariu", b =>
                {
                    b.Property<int>("cod_itinerariu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_itinerariu");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_itinerariu"));

                    b.Property<int?>("cod_cazare")
                        .HasColumnType("int")
                        .HasColumnName("cod_cazare");

                    b.Property<int>("cod_tur")
                        .HasColumnType("int")
                        .HasColumnName("cod_tur");

                    b.Property<string>("desc_itin")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("desc_itin");

                    b.Property<string>("img_itin")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("img_itin");

                    b.Property<string>("titlu_itin")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("titlu_itin");

                    b.Property<byte>("zi_activitate")
                        .HasColumnType("tinyint")
                        .HasColumnName("zi_activitate");

                    b.HasKey("cod_itinerariu");

                    b.HasIndex("cod_cazare");

                    b.HasIndex("cod_tur");

                    b.ToTable("itinerarii");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Oferta", b =>
                {
                    b.Property<int>("cod_oferta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_oferta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_oferta"));

                    b.Property<byte>("cod_punct")
                        .HasColumnType("tinyint")
                        .HasColumnName("cod_punct");

                    b.Property<int>("cod_tur")
                        .HasColumnType("int")
                        .HasColumnName("cod_tur");

                    b.Property<DateOnly>("data_intoarcere")
                        .HasColumnType("date")
                        .HasColumnName("data_intoarcere");

                    b.Property<DateOnly>("data_plecare")
                        .HasColumnType("date")
                        .HasColumnName("data_plecare");

                    b.Property<byte>("loc_libere")
                        .HasColumnType("tinyint")
                        .HasColumnName("loc_libere");

                    b.Property<decimal>("pret_adult")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("pret_adult");

                    b.Property<decimal>("pret_copil")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("pret_copil");

                    b.Property<bool>("tip_transport")
                        .HasColumnType("bit")
                        .HasColumnName("tip_transport");

                    b.HasKey("cod_oferta");

                    b.HasIndex("cod_punct");

                    b.HasIndex("cod_tur");

                    b.ToTable("oferte");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Plata", b =>
                {
                    b.Property<int>("cod_plata")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_plata");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_plata"));

                    b.Property<int>("cod_rezervare")
                        .HasColumnType("int")
                        .HasColumnName("cod_rezervare");

                    b.Property<DateTime>("creat_la")
                        .HasColumnType("datetime")
                        .HasColumnName("creat_la");

                    b.Property<DateTime?>("modificat_la")
                        .HasColumnType("datetime")
                        .HasColumnName("modificat_la");

                    b.Property<byte?>("nr_rate")
                        .HasColumnType("tinyint")
                        .HasColumnName("nr_rate");

                    b.Property<byte>("status_plata")
                        .HasColumnType("tinyint")
                        .HasColumnName("status_plata");

                    b.Property<string>("stripe_intent_id")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("stripe_charge_id");

                    b.Property<string>("stripe_method_id")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("stripe_currency");

                    b.Property<decimal>("suma_plata")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateOnly>("termen_plata")
                        .HasColumnType("date")
                        .HasColumnName("termen_plata");

                    b.Property<bool>("tip_plata")
                        .HasColumnType("bit")
                        .HasColumnName("tip_plata");

                    b.HasKey("cod_plata");

                    b.HasIndex("cod_rezervare");

                    b.ToTable("Plati");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Punct_Plecare", b =>
                {
                    b.Property<byte>("cod_punct")
                        .HasColumnType("tinyint")
                        .HasColumnName("cod_punct");

                    b.Property<string>("adresa")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("adresa");

                    b.Property<string>("judet")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("judet");

                    b.Property<string>("localitate")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("localitate");

                    b.Property<bool>("tip_transport")
                        .HasColumnType("bit")
                        .HasColumnName("tip_transport");

                    b.HasKey("cod_punct");

                    b.ToTable("puncte_plecare");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Rezervare", b =>
                {
                    b.Property<int>("cod_rezervare")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_rezervare");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_rezervare"));

                    b.Property<int>("cod_oferta")
                        .HasColumnType("int")
                        .HasColumnName("cod_oferta");

                    b.Property<DateOnly>("data_rezervare")
                        .HasColumnType("date")
                        .HasColumnName("data_rezervare");

                    b.Property<int?>("nr_utilizator")
                        .HasColumnType("int")
                        .HasColumnName("nr_utilizator");

                    b.Property<byte>("status_rezervare")
                        .HasColumnType("tinyint")
                        .HasColumnName("status_rezervare");

                    b.HasKey("cod_rezervare");

                    b.HasIndex("cod_oferta");

                    b.HasIndex("nr_utilizator");

                    b.ToTable("Rezervari");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Tara", b =>
                {
                    b.Property<byte>("cod_tara")
                        .HasColumnType("tinyint")
                        .HasColumnName("cod_tara");

                    b.Property<int>("continent")
                        .HasColumnType("int")
                        .HasColumnName("cod_continent");

                    b.Property<string>("den_tara")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("den_tara");

                    b.Property<string>("desc_tara")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("desc_tara");

                    b.Property<string>("img_tara")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("img_tara");

                    b.Property<bool>("pass")
                        .HasColumnType("bit")
                        .HasColumnName("pass");

                    b.HasKey("cod_tara");

                    b.ToTable("tari");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Tur", b =>
                {
                    b.Property<int>("cod_tur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_tur");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_tur"));

                    b.Property<string>("den_tur")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("den_tur");

                    b.Property<string>("desc_tur")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("desc_tur");

                    b.Property<byte>("grup_maxim")
                        .HasColumnType("tinyint")
                        .HasColumnName("grup_maxim");

                    b.Property<byte>("grup_minim")
                        .HasColumnType("tinyint")
                        .HasColumnName("grup_minim");

                    b.Property<string>("img_tur")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("img_tur");

                    b.Property<byte>("sol_fiz")
                        .HasColumnType("tinyint")
                        .HasColumnName("solicitare_fizica");

                    b.HasKey("cod_tur");

                    b.ToTable("tururi");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Tur_categorie", b =>
                {
                    b.Property<int>("cod_tur_categ")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod_tur_categ");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cod_tur_categ"));

                    b.Property<byte>("cod_categ")
                        .HasColumnType("tinyint")
                        .HasColumnName("cod_categ");

                    b.Property<int>("cod_tur")
                        .HasColumnType("int")
                        .HasColumnName("cod_tur");

                    b.HasKey("cod_tur_categ");

                    b.HasIndex("cod_categ");

                    b.HasIndex("cod_tur");

                    b.ToTable("tururi_categorii");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Utilizator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("nr_utilizator");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int")
                        .HasColumnName("incercari_esuate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token_concurenta");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("email_confirmat");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("blocare_activata");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("blocare_pana_la");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("email_normalizat");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("nume_normalizat");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("parola");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nr_telefon");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("telefon_confirmat");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token_securitate");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("auth_doi_factori");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("nume");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[nume_normalizat] IS NOT NULL");

                    b.ToTable("utilizatori", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roluri", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("CereriRol", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CereriUtilizator", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("LoginuriUtilizator", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UtilizatorRoluri", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("TokenuriUtilizator", (string)null);
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Beneficiar", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Camera", "Camera")
                        .WithMany()
                        .HasForeignKey("cod_camera")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Camera");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Camera", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Rezervare", "Rezervare")
                        .WithMany()
                        .HasForeignKey("cod_rezervare")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rezervare");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Cazare", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Destinatie", "Destinatie")
                        .WithMany()
                        .HasForeignKey("cod_destinatie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destinatie");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Destinatie", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Tara", "Tara")
                        .WithMany()
                        .HasForeignKey("cod_tara")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tara");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Destinatie_itinerariu", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Destinatie", "Destinatie")
                        .WithMany()
                        .HasForeignKey("cod_destinatie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lucrare_de_licenta.Models.Itinerariu", "Itinerariu")
                        .WithMany()
                        .HasForeignKey("cod_itinerariu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destinatie");

                    b.Navigation("Itinerariu");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Factura", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Plata", "Plata")
                        .WithMany()
                        .HasForeignKey("cod_plata")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plata");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Itinerariu", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Cazare", "Cazare")
                        .WithMany()
                        .HasForeignKey("cod_cazare");

                    b.HasOne("Lucrare_de_licenta.Models.Tur", "Tur")
                        .WithMany()
                        .HasForeignKey("cod_tur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cazare");

                    b.Navigation("Tur");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Oferta", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Punct_Plecare", "Punct")
                        .WithMany()
                        .HasForeignKey("cod_punct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lucrare_de_licenta.Models.Tur", "Tur")
                        .WithMany()
                        .HasForeignKey("cod_tur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Punct");

                    b.Navigation("Tur");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Plata", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Rezervare", "Rezervare")
                        .WithMany()
                        .HasForeignKey("cod_rezervare")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rezervare");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Rezervare", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Oferta", "Oferta")
                        .WithMany()
                        .HasForeignKey("cod_oferta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lucrare_de_licenta.Models.Utilizator", "Utilizator")
                        .WithMany()
                        .HasForeignKey("nr_utilizator");

                    b.Navigation("Oferta");

                    b.Navigation("Utilizator");
                });

            modelBuilder.Entity("Lucrare_de_licenta.Models.Tur_categorie", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("cod_categ")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lucrare_de_licenta.Models.Tur", "Tur")
                        .WithMany()
                        .HasForeignKey("cod_tur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");

                    b.Navigation("Tur");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Utilizator", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Utilizator", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lucrare_de_licenta.Models.Utilizator", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Lucrare_de_licenta.Models.Utilizator", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
