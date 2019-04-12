using System;
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    LiabilityInfo = table.Column<string>(nullable: true),
                    DocumentInfo = table.Column<string>(nullable: true),
                    PublishingPlace = table.Column<string>(nullable: true),
                    PublishingHouseName = table.Column<string>(nullable: true),
                    PublishingYear = table.Column<int>(nullable: false),
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
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
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
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    AdministratorId = table.Column<string>(nullable: true)
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
                name: "UserProfilesArticles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<string>(nullable: true),
                    ArticleId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_UserProfilesArticles_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    HeadOfDepartmentId = table.Column<string>(nullable: true),
                    FacultyReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentReports_FacultyReports_FacultyReportId",
                        column: x => x.FacultyReportId,
                        principalTable: "FacultyReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentReports_AspNetUsers_HeadOfDepartmentId",
                        column: x => x.HeadOfDepartmentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Topic = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DepartmentReportId = table.Column<int>(nullable: true)
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
                name: "TeacherReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    TeacherId = table.Column<string>(nullable: true),
                    DepartmentReportId = table.Column<int>(nullable: true)
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
                name: "Grants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeacherReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grants_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberOf = table.Column<int>(nullable: false),
                    MembershipInfo = table.Column<string>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oppositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    About = table.Column<string>(nullable: true),
                    DateOfOpposition = table.Column<DateTime>(nullable: false),
                    OpponentId = table.Column<string>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Oppositions_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TeacherReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentLicenseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentLicenseActivities_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostgraduateDissertationGuidances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuideId = table.Column<string>(nullable: true),
                    PostgraduateName = table.Column<string>(nullable: true),
                    Dissertation = table.Column<string>(nullable: true),
                    Speciality = table.Column<string>(nullable: true),
                    DateDegreeGained = table.Column<DateTime>(nullable: false),
                    GraduationYear = table.Column<int>(nullable: false),
                    TeacherReportId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_PostgraduateDissertationGuidances_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostgraduateGuidances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuideId = table.Column<string>(nullable: true),
                    PostgraduateName = table.Column<string>(nullable: true),
                    PostgraduateInfo = table.Column<string>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_PostgraduateGuidances_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    PublishingPlace = table.Column<string>(nullable: true),
                    PublishingHouseName = table.Column<string>(nullable: true),
                    PublishingYear = table.Column<int>(nullable: false),
                    PagesAmount = table.Column<int>(nullable: false),
                    IsPrintCanceled = table.Column<bool>(nullable: false),
                    IsRecommendedToPrint = table.Column<bool>(nullable: false),
                    TeacherReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportTheses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Thesis = table.Column<string>(nullable: true),
                    ConferenceId = table.Column<int>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ReportTheses_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuideId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DissertationTitle = table.Column<string>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ScientificConsultations_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificInternships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaceOfInternship = table.Column<string>(nullable: true),
                    Started = table.Column<DateTime>(nullable: false),
                    Ended = table.Column<DateTime>(nullable: false),
                    Contents = table.Column<string>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificInternships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificInternships_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cypher = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Contents = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ScientificWorks_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesGrants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserProfileId = table.Column<int>(nullable: false),
                    UserProfileId1 = table.Column<string>(nullable: true),
                    GrantId = table.Column<int>(nullable: false)
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
                        name: "FK_UserProfilesGrants_AspNetUsers_UserProfileId1",
                        column: x => x.UserProfileId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantsPatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicantId = table.Column<string>(nullable: true),
                    PatentLicenseActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantsPatentLicenseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantsPatentLicenseActivities_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicantsPatentLicenseActivities_PatentLicenseActivities_PatentLicenseActivityId",
                        column: x => x.PatentLicenseActivityId,
                        principalTable: "PatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorsPatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<int>(nullable: false),
                    AuthorId1 = table.Column<string>(nullable: true),
                    PatentLicenseActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsPatentLicenseActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorsPatentLicenseActivities_AspNetUsers_AuthorId1",
                        column: x => x.AuthorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthorsPatentLicenseActivities_PatentLicenseActivities_PatentLicenseActivityId",
                        column: x => x.PatentLicenseActivityId,
                        principalTable: "PatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkId = table.Column<string>(nullable: true),
                    DateOfReview = table.Column<DateTime>(nullable: false),
                    TeacherReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
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
                name: "UserProfilesPublications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserProfileId = table.Column<string>(nullable: true),
                    PublicationId = table.Column<int>(nullable: false),
                    PublicationId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesPublications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesPublications_Publications_PublicationId1",
                        column: x => x.PublicationId1,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfilesPublications_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesReportTheses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserProfileId = table.Column<int>(nullable: false),
                    UserProfileId1 = table.Column<string>(nullable: true),
                    ReportThesisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesReportTheses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesReportTheses_ReportTheses_ReportThesisId",
                        column: x => x.ReportThesisId,
                        principalTable: "ReportTheses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfilesReportTheses_AspNetUsers_UserProfileId1",
                        column: x => x.UserProfileId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesScientificInternships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserProfileId = table.Column<int>(nullable: false),
                    UserProfileId1 = table.Column<string>(nullable: true),
                    ScientificInternshipId = table.Column<int>(nullable: false)
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
                        name: "FK_UserProfilesScientificInternships_AspNetUsers_UserProfileId1",
                        column: x => x.UserProfileId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesScientificWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserProfileId = table.Column<string>(nullable: true),
                    ScientificWorkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesScientificWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesScientificWorks_ScientificWorks_ScientificWorkId",
                        column: x => x.ScientificWorkId,
                        principalTable: "ScientificWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfilesScientificWorks_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReviewerId = table.Column<int>(nullable: false),
                    ReviewerId1 = table.Column<string>(nullable: true),
                    ReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfilesReviews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfilesReviews_AspNetUsers_ReviewerId1",
                        column: x => x.ReviewerId1,
                        principalTable: "AspNetUsers",
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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsPatentLicenseActivities_AuthorId1",
                table: "AuthorsPatentLicenseActivities",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsPatentLicenseActivities_PatentLicenseActivityId",
                table: "AuthorsPatentLicenseActivities",
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
                name: "IX_FacultyReports_AdministratorId",
                table: "FacultyReports",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grants_TeacherReportId",
                table: "Grants",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_TeacherReportId",
                table: "Membership",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Oppositions_OpponentId",
                table: "Oppositions",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Oppositions_TeacherReportId",
                table: "Oppositions",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentLicenseActivities_TeacherReportId",
                table: "PatentLicenseActivities",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PostgraduateDissertationGuidances_GuideId",
                table: "PostgraduateDissertationGuidances",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_PostgraduateDissertationGuidances_TeacherReportId",
                table: "PostgraduateDissertationGuidances",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PostgraduateGuidances_GuideId",
                table: "PostgraduateGuidances",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_PostgraduateGuidances_TeacherReportId",
                table: "PostgraduateGuidances",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_TeacherReportId",
                table: "Publications",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTheses_ConferenceId",
                table: "ReportTheses",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTheses_TeacherReportId",
                table: "ReportTheses",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TeacherReportId",
                table: "Reviews",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WorkId",
                table: "Reviews",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificConsultations_GuideId",
                table: "ScientificConsultations",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificConsultations_TeacherReportId",
                table: "ScientificConsultations",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificInternships_TeacherReportId",
                table: "ScientificInternships",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_DepartmentId",
                table: "ScientificWorks",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_TeacherReportId",
                table: "ScientificWorks",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReports_DepartmentReportId",
                table: "TeacherReports",
                column: "DepartmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReports_TeacherId",
                table: "TeacherReports",
                column: "TeacherId");

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
                name: "IX_UserProfilesGrants_UserProfileId1",
                table: "UserProfilesGrants",
                column: "UserProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesPublications_PublicationId1",
                table: "UserProfilesPublications",
                column: "PublicationId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesPublications_UserProfileId",
                table: "UserProfilesPublications",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesReportTheses_ReportThesisId",
                table: "UserProfilesReportTheses",
                column: "ReportThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesReportTheses_UserProfileId1",
                table: "UserProfilesReportTheses",
                column: "UserProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesReviews_ReviewId",
                table: "UserProfilesReviews",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesReviews_ReviewerId1",
                table: "UserProfilesReviews",
                column: "ReviewerId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificInternships_ScientificInternshipId",
                table: "UserProfilesScientificInternships",
                column: "ScientificInternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificInternships_UserProfileId1",
                table: "UserProfilesScientificInternships",
                column: "UserProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificWorks_ScientificWorkId",
                table: "UserProfilesScientificWorks",
                column: "ScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilesScientificWorks_UserProfileId",
                table: "UserProfilesScientificWorks",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Oppositions");

            migrationBuilder.DropTable(
                name: "PostgraduateDissertationGuidances");

            migrationBuilder.DropTable(
                name: "PostgraduateGuidances");

            migrationBuilder.DropTable(
                name: "ScientificConsultations");

            migrationBuilder.DropTable(
                name: "UserProfilesArticles");

            migrationBuilder.DropTable(
                name: "UserProfilesGrants");

            migrationBuilder.DropTable(
                name: "UserProfilesPublications");

            migrationBuilder.DropTable(
                name: "UserProfilesReportTheses");

            migrationBuilder.DropTable(
                name: "UserProfilesReviews");

            migrationBuilder.DropTable(
                name: "UserProfilesScientificInternships");

            migrationBuilder.DropTable(
                name: "UserProfilesScientificWorks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PatentLicenseActivities");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Grants");

            migrationBuilder.DropTable(
                name: "ReportTheses");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ScientificInternships");

            migrationBuilder.DropTable(
                name: "ScientificWorks");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "TeacherReports");

            migrationBuilder.DropTable(
                name: "DepartmentReports");

            migrationBuilder.DropTable(
                name: "FacultyReports");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
