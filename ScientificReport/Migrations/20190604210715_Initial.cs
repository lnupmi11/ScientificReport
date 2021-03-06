﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificReport.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PublishingYear = table.Column<int>(nullable: false),
                    ArticleType = table.Column<int>(nullable: false),
                    LiabilityInfo = table.Column<string>(nullable: true),
                    DocumentInfo = table.Column<string>(nullable: true),
                    PublishingPlace = table.Column<string>(nullable: true),
                    PublishingHouseName = table.Column<string>(nullable: true),
                    IsPeriodical = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    PagesAmount = table.Column<int>(nullable: false),
                    IsPrintCanceled = table.Column<bool>(nullable: false),
                    IsRecommendedToPrint = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentLicenseActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PublishingYear = table.Column<int>(nullable: false),
                    PublicationType = table.Column<int>(nullable: false),
                    Specification = table.Column<string>(nullable: true),
                    PublishingPlace = table.Column<string>(nullable: true),
                    PublishingHouseName = table.Column<string>(nullable: true),
                    PagesAmount = table.Column<int>(nullable: false),
                    PrintStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScientificInternships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlaceOfInternship = table.Column<string>(nullable: true),
                    Started = table.Column<DateTime>(nullable: false),
                    Ended = table.Column<DateTime>(nullable: false),
                    Contents = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificInternships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoApplicantsPatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CoApplicant = table.Column<string>(nullable: true),
                    PatentLicenseActivityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoApplicantsPatentLicenseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoApplicantsPatentLicenseActivities_PatentLicenseActivities_PatentLicenseActivityId",
                        column: x => x.PatentLicenseActivityId,
                        principalTable: "PatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoauthorsPatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Coauthor = table.Column<string>(nullable: true),
                    PatentLicenseActivityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoauthorsPatentLicenseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoauthorsPatentLicenseActivities_PatentLicenseActivities_PatentLicenseActivityId",
                        column: x => x.PatentLicenseActivityId,
                        principalTable: "PatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    ArticleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsArticles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    ArticleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesArticles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantsPatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicantId = table.Column<Guid>(nullable: false),
                    PatentLicenseActivityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantsPatentLicenseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantsPatentLicenseActivities_PatentLicenseActivities_PatentLicenseActivityId",
                        column: x => x.PatentLicenseActivityId,
                        principalTable: "PatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "AuthorsPatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    PatentLicenseActivityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsPatentLicenseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorsPatentLicenseActivities_PatentLicenseActivities_PatentLicenseActivityId",
                        column: x => x.PatentLicenseActivityId,
                        principalTable: "PatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    HeadOfDepartmentId = table.Column<Guid>(nullable: true),
                    FacultyReportId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DepartmentReportId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conferences_DepartmentReports_DepartmentReportId",
                        column: x => x.DepartmentReportId,
                        principalTable: "DepartmentReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportTheses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Thesis = table.Column<string>(nullable: true),
                    ConferenceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTheses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTheses_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    HeadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthYear = table.Column<int>(nullable: false),
                    GraduationYear = table.Column<int>(nullable: false),
                    ScientificDegree = table.Column<string>(nullable: true),
                    YearDegreeGained = table.Column<int>(nullable: false),
                    AcademicStatus = table.Column<string>(nullable: true),
                    YearDegreeAssigned = table.Column<int>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PublishingYear = table.Column<int>(nullable: false),
                    Cypher = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Contents = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificWorks_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacultyReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    AdministratorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyReports_AspNetUsers_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    MembershipInfo = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oppositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    DateOfOpposition = table.Column<DateTime>(nullable: false),
                    OpponentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oppositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oppositions_AspNetUsers_OpponentId",
                        column: x => x.OpponentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostgraduateDissertationGuidances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GuideId = table.Column<Guid>(nullable: true),
                    PostgraduateName = table.Column<string>(nullable: true),
                    Dissertation = table.Column<string>(nullable: true),
                    Speciality = table.Column<string>(nullable: true),
                    DateDegreeGained = table.Column<DateTime>(nullable: false),
                    GraduationYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostgraduateDissertationGuidances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostgraduateDissertationGuidances_AspNetUsers_GuideId",
                        column: x => x.GuideId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostgraduateGuidances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GuideId = table.Column<Guid>(nullable: true),
                    PostgraduateName = table.Column<string>(nullable: true),
                    PostgraduateInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostgraduateGuidances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostgraduateGuidances_AspNetUsers_GuideId",
                        column: x => x.GuideId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkId = table.Column<Guid>(nullable: true),
                    DateOfReview = table.Column<DateTime>(nullable: false),
                    ReviewerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Publications_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificConsultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GuideId = table.Column<Guid>(nullable: true),
                    CandidateName = table.Column<string>(nullable: true),
                    DissertationTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificConsultations_AspNetUsers_GuideId",
                        column: x => x.GuideId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: true),
                    DepartmentReportId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReports_DepartmentReports_DepartmentReportId",
                        column: x => x.DepartmentReportId,
                        principalTable: "DepartmentReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReports_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: false),
                    GrantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesGrants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesGrants_Grants_GrantId",
                        column: x => x.GrantId,
                        principalTable: "Grants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfilesGrants_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesPublications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: false),
                    PublicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesPublications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesPublications_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfilesPublications_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesReportTheses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: true),
                    ReportThesisId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesReportTheses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesReportTheses_ReportTheses_ReportThesisId",
                        column: x => x.ReportThesisId,
                        principalTable: "ReportTheses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfilesReportTheses_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesScientificInternships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: false),
                    ScientificInternshipId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesScientificInternships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesScientificInternships_ScientificInternships_ScientificInternshipId",
                        column: x => x.ScientificInternshipId,
                        principalTable: "ScientificInternships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfilesScientificInternships_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesScientificWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: true),
                    ScientificWorkId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesScientificWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesScientificWorks_ScientificWorks_ScientificWorkId",
                        column: x => x.ScientificWorkId,
                        principalTable: "ScientificWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfilesScientificWorks_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    GrantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsGrants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsGrants_Grants_GrantId",
                        column: x => x.GrantId,
                        principalTable: "Grants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsGrants_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    MembershipId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsMemberships_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsMemberships_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsOppositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    OppositionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsOppositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsOppositions_Oppositions_OppositionId",
                        column: x => x.OppositionId,
                        principalTable: "Oppositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsOppositions_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsPatents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    PatentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsPatents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPatents_PatentLicenseActivities_PatentId",
                        column: x => x.PatentId,
                        principalTable: "PatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPatents_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsPostgraduateDissertationGuidances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    PostgraduateDissertationGuidanceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsPostgraduateDissertationGuidances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPostgraduateDissertationGuidances_PostgraduateDissertationGuidances_PostgraduateDissertationGuidanceId",
                        column: x => x.PostgraduateDissertationGuidanceId,
                        principalTable: "PostgraduateDissertationGuidances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPostgraduateDissertationGuidances_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsPostgraduateGuidances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    PostgraduateGuidanceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsPostgraduateGuidances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPostgraduateGuidances_PostgraduateGuidances_PostgraduateGuidanceId",
                        column: x => x.PostgraduateGuidanceId,
                        principalTable: "PostgraduateGuidances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPostgraduateGuidances_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsPublications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    PublicationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsPublications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPublications_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsPublications_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsReportThesis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    ReportThesisId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsReportThesis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsReportThesis_ReportTheses_ReportThesisId",
                        column: x => x.ReportThesisId,
                        principalTable: "ReportTheses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsReportThesis_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    ReviewId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsReviews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsReviews_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsScientificConsultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    ScientificConsultationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsScientificConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsScientificConsultations_ScientificConsultations_ScientificConsultationId",
                        column: x => x.ScientificConsultationId,
                        principalTable: "ScientificConsultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsScientificConsultations_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsScientificInternships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    ScientificInternshipId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsScientificInternships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsScientificInternships_ScientificInternships_ScientificInternshipId",
                        column: x => x.ScientificInternshipId,
                        principalTable: "ScientificInternships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsScientificInternships_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReportsScientificWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherReportId = table.Column<Guid>(nullable: true),
                    ScientificWorkId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportsScientificWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportsScientificWorks_ScientificWorks_ScientificWorkId",
                        column: x => x.ScientificWorkId,
                        principalTable: "ScientificWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherReportsScientificWorks_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantsPatentLicenseActivities_ApplicantId",
                table: "ApplicantsPatentLicenseActivities",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantsPatentLicenseActivities_PatentLicenseActivityId",
                table: "ApplicantsPatentLicenseActivities",
                column: "PatentLicenseActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsPatentLicenseActivities_AuthorId",
                table: "AuthorsPatentLicenseActivities",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsPatentLicenseActivities_PatentLicenseActivityId",
                table: "AuthorsPatentLicenseActivities",
                column: "PatentLicenseActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CoApplicantsPatentLicenseActivities_PatentLicenseActivityId",
                table: "CoApplicantsPatentLicenseActivities",
                column: "PatentLicenseActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CoauthorsPatentLicenseActivities_PatentLicenseActivityId",
                table: "CoauthorsPatentLicenseActivities",
                column: "PatentLicenseActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_DepartmentReportId",
                table: "Conferences",
                column: "DepartmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentReports_FacultyReportId",
                table: "DepartmentReports",
                column: "FacultyReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentReports_HeadOfDepartmentId",
                table: "DepartmentReports",
                column: "HeadOfDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadId",
                table: "Departments",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyReports_AdministratorId",
                table: "FacultyReports",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserId",
                table: "Memberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Oppositions_OpponentId",
                table: "Oppositions",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostgraduateDissertationGuidances_GuideId",
                table: "PostgraduateDissertationGuidances",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_PostgraduateGuidances_GuideId",
                table: "PostgraduateGuidances",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTheses_ConferenceId",
                table: "ReportTheses",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WorkId",
                table: "Reviews",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificConsultations_GuideId",
                table: "ScientificConsultations",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_DepartmentId",
                table: "ScientificWorks",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReports_DepartmentReportId",
                table: "TeacherReports",
                column: "DepartmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReports_TeacherId",
                table: "TeacherReports",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsArticles_ArticleId",
                table: "TeacherReportsArticles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsArticles_TeacherReportId",
                table: "TeacherReportsArticles",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsGrants_GrantId",
                table: "TeacherReportsGrants",
                column: "GrantId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsGrants_TeacherReportId",
                table: "TeacherReportsGrants",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsMemberships_MembershipId",
                table: "TeacherReportsMemberships",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsMemberships_TeacherReportId",
                table: "TeacherReportsMemberships",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsOppositions_OppositionId",
                table: "TeacherReportsOppositions",
                column: "OppositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsOppositions_TeacherReportId",
                table: "TeacherReportsOppositions",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPatents_PatentId",
                table: "TeacherReportsPatents",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPatents_TeacherReportId",
                table: "TeacherReportsPatents",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPostgraduateDissertationGuidances_PostgraduateDissertationGuidanceId",
                table: "TeacherReportsPostgraduateDissertationGuidances",
                column: "PostgraduateDissertationGuidanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPostgraduateDissertationGuidances_TeacherReportId",
                table: "TeacherReportsPostgraduateDissertationGuidances",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPostgraduateGuidances_PostgraduateGuidanceId",
                table: "TeacherReportsPostgraduateGuidances",
                column: "PostgraduateGuidanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPostgraduateGuidances_TeacherReportId",
                table: "TeacherReportsPostgraduateGuidances",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPublications_PublicationId",
                table: "TeacherReportsPublications",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsPublications_TeacherReportId",
                table: "TeacherReportsPublications",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsReportThesis_ReportThesisId",
                table: "TeacherReportsReportThesis",
                column: "ReportThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsReportThesis_TeacherReportId",
                table: "TeacherReportsReportThesis",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsReviews_ReviewId",
                table: "TeacherReportsReviews",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsReviews_TeacherReportId",
                table: "TeacherReportsReviews",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsScientificConsultations_ScientificConsultationId",
                table: "TeacherReportsScientificConsultations",
                column: "ScientificConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsScientificConsultations_TeacherReportId",
                table: "TeacherReportsScientificConsultations",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsScientificInternships_ScientificInternshipId",
                table: "TeacherReportsScientificInternships",
                column: "ScientificInternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsScientificInternships_TeacherReportId",
                table: "TeacherReportsScientificInternships",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsScientificWorks_ScientificWorkId",
                table: "TeacherReportsScientificWorks",
                column: "ScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportsScientificWorks_TeacherReportId",
                table: "TeacherReportsScientificWorks",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesArticles_ArticleId",
                table: "UserProfilesArticles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesArticles_AuthorId",
                table: "UserProfilesArticles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesGrants_GrantId",
                table: "UserProfilesGrants",
                column: "GrantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesGrants_UserProfileId",
                table: "UserProfilesGrants",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesPublications_PublicationId",
                table: "UserProfilesPublications",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesPublications_UserProfileId",
                table: "UserProfilesPublications",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesReportTheses_ReportThesisId",
                table: "UserProfilesReportTheses",
                column: "ReportThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesReportTheses_UserProfileId",
                table: "UserProfilesReportTheses",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificInternships_ScientificInternshipId",
                table: "UserProfilesScientificInternships",
                column: "ScientificInternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificInternships_UserProfileId",
                table: "UserProfilesScientificInternships",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificWorks_ScientificWorkId",
                table: "UserProfilesScientificWorks",
                column: "ScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificWorks_UserProfileId",
                table: "UserProfilesScientificWorks",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherReportsArticles_TeacherReports_TeacherReportId",
                table: "TeacherReportsArticles",
                column: "TeacherReportId",
                principalTable: "TeacherReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfilesArticles_AspNetUsers_AuthorId",
                table: "UserProfilesArticles",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantsPatentLicenseActivities_AspNetUsers_ApplicantId",
                table: "ApplicantsPatentLicenseActivities",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsPatentLicenseActivities_AspNetUsers_AuthorId",
                table: "AuthorsPatentLicenseActivities",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentReports_AspNetUsers_HeadOfDepartmentId",
                table: "DepartmentReports",
                column: "HeadOfDepartmentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentReports_FacultyReports_FacultyReportId",
                table: "DepartmentReports",
                column: "FacultyReportId",
                principalTable: "FacultyReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_HeadId",
                table: "Departments",
                column: "HeadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_HeadId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "ApplicantsPatentLicenseActivities");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorsPatentLicenseActivities");

            migrationBuilder.DropTable(
                name: "CoApplicantsPatentLicenseActivities");

            migrationBuilder.DropTable(
                name: "CoauthorsPatentLicenseActivities");

            migrationBuilder.DropTable(
                name: "TeacherReportsArticles");

            migrationBuilder.DropTable(
                name: "TeacherReportsGrants");

            migrationBuilder.DropTable(
                name: "TeacherReportsMemberships");

            migrationBuilder.DropTable(
                name: "TeacherReportsOppositions");

            migrationBuilder.DropTable(
                name: "TeacherReportsPatents");

            migrationBuilder.DropTable(
                name: "TeacherReportsPostgraduateDissertationGuidances");

            migrationBuilder.DropTable(
                name: "TeacherReportsPostgraduateGuidances");

            migrationBuilder.DropTable(
                name: "TeacherReportsPublications");

            migrationBuilder.DropTable(
                name: "TeacherReportsReportThesis");

            migrationBuilder.DropTable(
                name: "TeacherReportsReviews");

            migrationBuilder.DropTable(
                name: "TeacherReportsScientificConsultations");

            migrationBuilder.DropTable(
                name: "TeacherReportsScientificInternships");

            migrationBuilder.DropTable(
                name: "TeacherReportsScientificWorks");

            migrationBuilder.DropTable(
                name: "UserProfilesArticles");

            migrationBuilder.DropTable(
                name: "UserProfilesGrants");

            migrationBuilder.DropTable(
                name: "UserProfilesPublications");

            migrationBuilder.DropTable(
                name: "UserProfilesReportTheses");

            migrationBuilder.DropTable(
                name: "UserProfilesScientificInternships");

            migrationBuilder.DropTable(
                name: "UserProfilesScientificWorks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Oppositions");

            migrationBuilder.DropTable(
                name: "PatentLicenseActivities");

            migrationBuilder.DropTable(
                name: "PostgraduateDissertationGuidances");

            migrationBuilder.DropTable(
                name: "PostgraduateGuidances");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ScientificConsultations");

            migrationBuilder.DropTable(
                name: "TeacherReports");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Grants");

            migrationBuilder.DropTable(
                name: "ReportTheses");

            migrationBuilder.DropTable(
                name: "ScientificInternships");

            migrationBuilder.DropTable(
                name: "ScientificWorks");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "DepartmentReports");

            migrationBuilder.DropTable(
                name: "FacultyReports");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
