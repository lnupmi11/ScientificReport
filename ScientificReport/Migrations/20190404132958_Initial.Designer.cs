﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScientificReport.Data;

namespace ScientificReport.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190404132958_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ScientificReport.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DocumentInfo")
                        .IsRequired();

                    b.Property<bool>("IsPeriodical");

                    b.Property<bool>("IsPrintCanceled");

                    b.Property<bool>("IsRecommendedToPrint");

                    b.Property<string>("LiabilityInfo")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.Property<int>("PagesAmount");

                    b.Property<string>("PublishingHouseName")
                        .IsRequired();

                    b.Property<string>("PublishingPlace")
                        .IsRequired();

                    b.Property<int>("PublishingYear");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ScientificReport.Models.Conference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("DepartmentReportId");

                    b.Property<string>("Topic")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DepartmentReportId");

                    b.ToTable("Conferences");
                });

            modelBuilder.Entity("ScientificReport.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ScientificReport.Models.DepartmentReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Edited");

                    b.Property<int?>("FacultyReportId");

                    b.Property<string>("HeadOfDepartmentId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FacultyReportId");

                    b.HasIndex("HeadOfDepartmentId");

                    b.ToTable("DepartmentReports");
                });

            modelBuilder.Entity("ScientificReport.Models.FacultyReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdministratorId")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Edited");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.ToTable("FacultyReports");
                });

            modelBuilder.Entity("ScientificReport.Models.Grant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TeacherReportId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("Grants");
                });

            modelBuilder.Entity("ScientificReport.Models.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MemberOf");

                    b.Property<string>("MembershipInfo")
                        .IsRequired();

                    b.Property<int?>("TeacherReportId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("Membership");
                });

            modelBuilder.Entity("ScientificReport.Models.Opposition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About")
                        .IsRequired();

                    b.Property<DateTime>("DateOfOpposition");

                    b.Property<string>("OpponentId")
                        .IsRequired();

                    b.Property<int?>("TeacherReportId");

                    b.HasKey("Id");

                    b.HasIndex("OpponentId");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("Oppositions");
                });

            modelBuilder.Entity("ScientificReport.Models.PatentLicenseActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.Property<int?>("TeacherReportId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("PatentLicenseActivities");
                });

            modelBuilder.Entity("ScientificReport.Models.PostgraduateDissertationGuidance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateDegreeGained");

                    b.Property<string>("Dissertation")
                        .IsRequired();

                    b.Property<int>("GraduationYear");

                    b.Property<string>("GuideId")
                        .IsRequired();

                    b.Property<string>("PostgraduateName")
                        .IsRequired();

                    b.Property<string>("Speciality")
                        .IsRequired();

                    b.Property<int?>("TeacherReportId");

                    b.HasKey("Id");

                    b.HasIndex("GuideId");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("PostgraduateDissertationGuidances");
                });

            modelBuilder.Entity("ScientificReport.Models.PostgraduateGuidance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GuideId")
                        .IsRequired();

                    b.Property<string>("PostgraduateInfo")
                        .IsRequired();

                    b.Property<string>("PostgraduateName")
                        .IsRequired();

                    b.Property<int?>("TeacherReportId");

                    b.HasKey("Id");

                    b.HasIndex("GuideId");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("PostgraduateGuidances");
                });

            modelBuilder.Entity("ScientificReport.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsPrintCanceled");

                    b.Property<bool>("IsRecommendedToPrint");

                    b.Property<int>("PagesAmount");

                    b.Property<string>("PublishingHouseName")
                        .IsRequired();

                    b.Property<string>("PublishingPlace")
                        .IsRequired();

                    b.Property<int>("PublishingYear");

                    b.Property<string>("Specification")
                        .IsRequired();

                    b.Property<int?>("TeacherReportId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("ScientificReport.Models.ReportThesis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConferenceId");

                    b.Property<int?>("TeacherReportId");

                    b.Property<string>("Thesis")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("ReportTheses");
                });

            modelBuilder.Entity("ScientificReport.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfReview");

                    b.Property<int?>("TeacherReportId");

                    b.Property<int>("WorkId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherReportId");

                    b.HasIndex("WorkId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ScientificReport.Models.ScientificConsultation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DissertationTitle")
                        .IsRequired();

                    b.Property<string>("GuideId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("TeacherReportId");

                    b.HasKey("Id");

                    b.HasIndex("GuideId");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("ScientificConsultations");
                });

            modelBuilder.Entity("ScientificReport.Models.ScientificInternship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contents")
                        .IsRequired();

                    b.Property<DateTime>("Ended");

                    b.Property<string>("PlaceOfInternship")
                        .IsRequired();

                    b.Property<DateTime>("Started");

                    b.Property<int?>("TeacherReportId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("ScientificInternships");
                });

            modelBuilder.Entity("ScientificReport.Models.ScientificWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("Contents")
                        .IsRequired();

                    b.Property<string>("Cypher")
                        .IsRequired();

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("TeacherReportId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TeacherReportId");

                    b.ToTable("ScientificWorks");
                });

            modelBuilder.Entity("ScientificReport.Models.TeacherReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("DepartmentReportId");

                    b.Property<DateTime>("Edited");

                    b.Property<string>("TeacherId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DepartmentReportId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherReports");
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfile", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcademicStatus")
                        .IsRequired();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("ArticleId");

                    b.Property<int>("BirthYear");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("GraduationYear");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<int?>("PatentLicenseActivityId");

                    b.Property<int?>("PatentLicenseActivityId1");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Position")
                        .IsRequired();

                    b.Property<int?>("ReportThesisId");

                    b.Property<int?>("ReviewId");

                    b.Property<string>("ScientificDegree")
                        .IsRequired();

                    b.Property<int?>("ScientificInternshipId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("YearDegreeAssigned");

                    b.Property<int>("YearDegreeGained");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("PatentLicenseActivityId");

                    b.HasIndex("PatentLicenseActivityId1");

                    b.HasIndex("ReportThesisId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("ScientificInternshipId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfilesGrants", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GrantId");

                    b.Property<string>("UserProfileId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GrantId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfilesGrants");
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfilesPublications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PublicationId");

                    b.Property<string>("UserProfileId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfilesPublications");
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfilesScientificWorks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ScientificWorkId");

                    b.Property<string>("UserProfileId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ScientificWorkId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfilesScientificWorks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.UserProfile")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScientificReport.Models.Conference", b =>
                {
                    b.HasOne("ScientificReport.Models.DepartmentReport")
                        .WithMany("Conferences")
                        .HasForeignKey("DepartmentReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.DepartmentReport", b =>
                {
                    b.HasOne("ScientificReport.Models.FacultyReport")
                        .WithMany("DepartmentReports")
                        .HasForeignKey("FacultyReportId");

                    b.HasOne("ScientificReport.Models.UserProfile", "HeadOfDepartment")
                        .WithMany()
                        .HasForeignKey("HeadOfDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScientificReport.Models.FacultyReport", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScientificReport.Models.Grant", b =>
                {
                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("Grants")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.Membership", b =>
                {
                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("Memberships")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.Opposition", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile", "Opponent")
                        .WithMany()
                        .HasForeignKey("OpponentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("Oppositions")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.PatentLicenseActivity", b =>
                {
                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("Patents")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.PostgraduateDissertationGuidance", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile", "Guide")
                        .WithMany()
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("PostgraduateDissertationGuidances")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.PostgraduateGuidance", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile", "Guide")
                        .WithMany()
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("PostgraduateGuidances")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.Publication", b =>
                {
                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("Publications")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.ReportThesis", b =>
                {
                    b.HasOne("ScientificReport.Models.Conference", "Conference")
                        .WithMany()
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("ReportTheses")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.Review", b =>
                {
                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("Reviews")
                        .HasForeignKey("TeacherReportId");

                    b.HasOne("ScientificReport.Models.Publication", "Work")
                        .WithMany()
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScientificReport.Models.ScientificConsultation", b =>
                {
                    b.HasOne("ScientificReport.Models.UserProfile", "Guide")
                        .WithMany()
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("ScientificConsultations")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.ScientificInternship", b =>
                {
                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("ScientificInternships")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.ScientificWork", b =>
                {
                    b.HasOne("ScientificReport.Models.Department")
                        .WithMany("ScientificWorks")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("ScientificReport.Models.TeacherReport")
                        .WithMany("ScientificWorks")
                        .HasForeignKey("TeacherReportId");
                });

            modelBuilder.Entity("ScientificReport.Models.TeacherReport", b =>
                {
                    b.HasOne("ScientificReport.Models.DepartmentReport")
                        .WithMany("TeacherReports")
                        .HasForeignKey("DepartmentReportId");

                    b.HasOne("ScientificReport.Models.UserProfile", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfile", b =>
                {
                    b.HasOne("ScientificReport.Models.Article")
                        .WithMany("Authors")
                        .HasForeignKey("ArticleId");

                    b.HasOne("ScientificReport.Models.PatentLicenseActivity")
                        .WithMany("Applicants")
                        .HasForeignKey("PatentLicenseActivityId");

                    b.HasOne("ScientificReport.Models.PatentLicenseActivity")
                        .WithMany("Authors")
                        .HasForeignKey("PatentLicenseActivityId1");

                    b.HasOne("ScientificReport.Models.ReportThesis")
                        .WithMany("Authors")
                        .HasForeignKey("ReportThesisId");

                    b.HasOne("ScientificReport.Models.Review")
                        .WithMany("Reviewers")
                        .HasForeignKey("ReviewId");

                    b.HasOne("ScientificReport.Models.ScientificInternship")
                        .WithMany("Users")
                        .HasForeignKey("ScientificInternshipId");
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfilesGrants", b =>
                {
                    b.HasOne("ScientificReport.Models.Grant", "Grant")
                        .WithMany("UserProfilesGrants")
                        .HasForeignKey("GrantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.UserProfile", "UserProfile")
                        .WithMany("UserProfilesGrants")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfilesPublications", b =>
                {
                    b.HasOne("ScientificReport.Models.Publication", "Publication")
                        .WithMany("UserProfilesPublications")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.UserProfile", "UserProfile")
                        .WithMany("UserProfilesPublications")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScientificReport.Models.UserProfilesScientificWorks", b =>
                {
                    b.HasOne("ScientificReport.Models.ScientificWork", "ScientificWork")
                        .WithMany("UserProfilesScientificWorks")
                        .HasForeignKey("ScientificWorkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificReport.Models.UserProfile", "UserProfile")
                        .WithMany("UserProfilesScientificWorks")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
