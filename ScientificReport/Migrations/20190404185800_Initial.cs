using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificReport.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesArticles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesGrants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesPatentLicenseActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesPatentLicenseActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesPublications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesPublications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesReportTheses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesReportTheses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesScientificInternships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesScientificInternships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfilesScientificWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilesScientificWorks", x => x.Id);
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
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    LiabilityInfo = table.Column<string>(nullable: false),
                    DocumentInfo = table.Column<string>(nullable: false),
                    PublishingPlace = table.Column<string>(nullable: false),
                    PublishingHouseName = table.Column<string>(nullable: false),
                    PublishingYear = table.Column<int>(nullable: false),
                    IsPeriodical = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    PagesAmount = table.Column<int>(nullable: false),
                    IsPrintCanceled = table.Column<bool>(nullable: false),
                    IsRecommendedToPrint = table.Column<bool>(nullable: false),
                    UserProfilesArticlesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_UserProfilesArticles_UserProfilesArticlesId",
                        column: x => x.UserProfilesArticlesId,
                        principalTable: "UserProfilesArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    BirthYear = table.Column<int>(nullable: false),
                    GraduationYear = table.Column<int>(nullable: false),
                    ScientificDegree = table.Column<string>(nullable: false),
                    YearDegreeGained = table.Column<int>(nullable: false),
                    AcademicStatus = table.Column<string>(nullable: false),
                    YearDegreeAssigned = table.Column<int>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    UserProfilesPublicationsId = table.Column<int>(nullable: true),
                    UserProfilesGrantsId = table.Column<int>(nullable: true),
                    UserProfilesScientificWorksId = table.Column<int>(nullable: true),
                    UserProfilesArticlesId = table.Column<int>(nullable: true),
                    UserProfilesReportThesesId = table.Column<int>(nullable: true),
                    UserProfilesReviewsId = table.Column<int>(nullable: true),
                    UserProfilesScientificInternshipsId = table.Column<int>(nullable: true),
                    UserProfilesPatentLicenseActivitiesId = table.Column<int>(nullable: true),
                    UserProfilesPatentLicenseActivitiesId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesArticles_UserProfilesArticlesId",
                        column: x => x.UserProfilesArticlesId,
                        principalTable: "UserProfilesArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesGrants_UserProfilesGrantsId",
                        column: x => x.UserProfilesGrantsId,
                        principalTable: "UserProfilesGrants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesPatentLicenseActivities_UserProfilesPatentLicenseActivitiesId",
                        column: x => x.UserProfilesPatentLicenseActivitiesId,
                        principalTable: "UserProfilesPatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesPatentLicenseActivities_UserProfilesPatentLicenseActivitiesId1",
                        column: x => x.UserProfilesPatentLicenseActivitiesId1,
                        principalTable: "UserProfilesPatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesPublications_UserProfilesPublicationsId",
                        column: x => x.UserProfilesPublicationsId,
                        principalTable: "UserProfilesPublications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesReportTheses_UserProfilesReportThesesId",
                        column: x => x.UserProfilesReportThesesId,
                        principalTable: "UserProfilesReportTheses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesReviews_UserProfilesReviewsId",
                        column: x => x.UserProfilesReviewsId,
                        principalTable: "UserProfilesReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesScientificInternships_UserProfilesScientificInternshipsId",
                        column: x => x.UserProfilesScientificInternshipsId,
                        principalTable: "UserProfilesScientificInternships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfilesScientificWorks_UserProfilesScientificWorksId",
                        column: x => x.UserProfilesScientificWorksId,
                        principalTable: "UserProfilesScientificWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    AdministratorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyReports_AspNetUsers_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    HeadOfDepartmentId = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Topic = table.Column<string>(nullable: false),
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
                    TeacherId = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserProfilesGrantsId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Grants_UserProfilesGrants_UserProfilesGrantsId",
                        column: x => x.UserProfilesGrantsId,
                        principalTable: "UserProfilesGrants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberOf = table.Column<int>(nullable: false),
                    MembershipInfo = table.Column<string>(nullable: false),
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
                    About = table.Column<string>(nullable: false),
                    DateOfOpposition = table.Column<DateTime>(nullable: false),
                    OpponentId = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oppositions_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatentLicenseActivity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UserProfilesPatentLicenseActivitiesId = table.Column<int>(nullable: true),
                    TeacherReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentLicenseActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentLicenseActivity_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatentLicenseActivity_UserProfilesPatentLicenseActivities_UserProfilesPatentLicenseActivitiesId",
                        column: x => x.UserProfilesPatentLicenseActivitiesId,
                        principalTable: "UserProfilesPatentLicenseActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostgraduateDissertationGuidances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuideId = table.Column<string>(nullable: false),
                    PostgraduateName = table.Column<string>(nullable: false),
                    Dissertation = table.Column<string>(nullable: false),
                    Speciality = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                    GuideId = table.Column<string>(nullable: false),
                    PostgraduateName = table.Column<string>(nullable: false),
                    PostgraduateInfo = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Specification = table.Column<string>(nullable: false),
                    PublishingPlace = table.Column<string>(nullable: false),
                    PublishingHouseName = table.Column<string>(nullable: false),
                    PublishingYear = table.Column<int>(nullable: false),
                    PagesAmount = table.Column<int>(nullable: false),
                    IsPrintCanceled = table.Column<bool>(nullable: false),
                    IsRecommendedToPrint = table.Column<bool>(nullable: false),
                    UserProfilesPublicationsId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Publications_UserProfilesPublications_UserProfilesPublicationsId",
                        column: x => x.UserProfilesPublicationsId,
                        principalTable: "UserProfilesPublications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTheses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Thesis = table.Column<string>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false),
                    UserProfilesReportThesesId = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTheses_TeacherReports_TeacherReportId",
                        column: x => x.TeacherReportId,
                        principalTable: "TeacherReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportTheses_UserProfilesReportTheses_UserProfilesReportThesesId",
                        column: x => x.UserProfilesReportThesesId,
                        principalTable: "UserProfilesReportTheses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuideId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DissertationTitle = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                    PlaceOfInternship = table.Column<string>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Ended = table.Column<DateTime>(nullable: false),
                    Contents = table.Column<string>(nullable: false),
                    UserProfilesScientificInternshipsId = table.Column<int>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ScientificInternships_UserProfilesScientificInternships_UserProfilesScientificInternshipsId",
                        column: x => x.UserProfilesScientificInternshipsId,
                        principalTable: "UserProfilesScientificInternships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cypher = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Contents = table.Column<string>(nullable: false),
                    UserProfilesScientificWorksId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ScientificWorks_UserProfilesScientificWorks_UserProfilesScientificWorksId",
                        column: x => x.UserProfilesScientificWorksId,
                        principalTable: "UserProfilesScientificWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkId = table.Column<int>(nullable: false),
                    DateOfReview = table.Column<DateTime>(nullable: false),
                    UserProfilesReviewsId = table.Column<int>(nullable: true),
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
                        name: "FK_Reviews_UserProfilesReviews_UserProfilesReviewsId",
                        column: x => x.UserProfilesReviewsId,
                        principalTable: "UserProfilesReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Publications_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserProfilesArticlesId",
                table: "Articles",
                column: "UserProfilesArticlesId");

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
                name: "IX_AspNetUsers_UserProfilesArticlesId",
                table: "AspNetUsers",
                column: "UserProfilesArticlesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesGrantsId",
                table: "AspNetUsers",
                column: "UserProfilesGrantsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesPatentLicenseActivitiesId",
                table: "AspNetUsers",
                column: "UserProfilesPatentLicenseActivitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesPatentLicenseActivitiesId1",
                table: "AspNetUsers",
                column: "UserProfilesPatentLicenseActivitiesId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesPublicationsId",
                table: "AspNetUsers",
                column: "UserProfilesPublicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesReportThesesId",
                table: "AspNetUsers",
                column: "UserProfilesReportThesesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesReviewsId",
                table: "AspNetUsers",
                column: "UserProfilesReviewsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesScientificInternshipsId",
                table: "AspNetUsers",
                column: "UserProfilesScientificInternshipsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfilesScientificWorksId",
                table: "AspNetUsers",
                column: "UserProfilesScientificWorksId");

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
                name: "IX_Grants_UserProfilesGrantsId",
                table: "Grants",
                column: "UserProfilesGrantsId");

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
                name: "IX_PatentLicenseActivity_TeacherReportId",
                table: "PatentLicenseActivity",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentLicenseActivity_UserProfilesPatentLicenseActivitiesId",
                table: "PatentLicenseActivity",
                column: "UserProfilesPatentLicenseActivitiesId");

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
                name: "IX_Publications_UserProfilesPublicationsId",
                table: "Publications",
                column: "UserProfilesPublicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTheses_ConferenceId",
                table: "ReportTheses",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTheses_TeacherReportId",
                table: "ReportTheses",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTheses_UserProfilesReportThesesId",
                table: "ReportTheses",
                column: "UserProfilesReportThesesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TeacherReportId",
                table: "Reviews",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserProfilesReviewsId",
                table: "Reviews",
                column: "UserProfilesReviewsId");

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
                name: "IX_ScientificInternships_UserProfilesScientificInternshipsId",
                table: "ScientificInternships",
                column: "UserProfilesScientificInternshipsId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_DepartmentId",
                table: "ScientificWorks",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_TeacherReportId",
                table: "ScientificWorks",
                column: "TeacherReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_UserProfilesScientificWorksId",
                table: "ScientificWorks",
                column: "UserProfilesScientificWorksId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReports_DepartmentReportId",
                table: "TeacherReports",
                column: "DepartmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReports_TeacherId",
                table: "TeacherReports",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

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
                name: "Grants");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Oppositions");

            migrationBuilder.DropTable(
                name: "PatentLicenseActivity");

            migrationBuilder.DropTable(
                name: "PostgraduateDissertationGuidances");

            migrationBuilder.DropTable(
                name: "PostgraduateGuidances");

            migrationBuilder.DropTable(
                name: "ReportTheses");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ScientificConsultations");

            migrationBuilder.DropTable(
                name: "ScientificInternships");

            migrationBuilder.DropTable(
                name: "ScientificWorks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

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

            migrationBuilder.DropTable(
                name: "UserProfilesArticles");

            migrationBuilder.DropTable(
                name: "UserProfilesGrants");

            migrationBuilder.DropTable(
                name: "UserProfilesPatentLicenseActivities");

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
        }
    }
}
