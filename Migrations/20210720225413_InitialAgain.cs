using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapp.Migrations
{
    public partial class InitialAgain : Migration
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
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    RequestId = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    RequestPath = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    ActionDescriptor = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TotalPrice = table.Column<float>(nullable: false),
                    TotalDiscount = table.Column<float>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopUpCard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VenueType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gear",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    GearTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gear_GearType_GearTypeId",
                        column: x => x.GearTypeId,
                        principalTable: "GearType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(maxLength: 128, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GearImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageLink = table.Column<string>(nullable: true),
                    ContainerW = table.Column<float>(nullable: false),
                    ContainerH = table.Column<float>(nullable: false),
                    TranslateX = table.Column<float>(nullable: false),
                    TranslateY = table.Column<float>(nullable: false),
                    ScaleX = table.Column<float>(nullable: false),
                    ScaleY = table.Column<float>(nullable: false),
                    SkewX = table.Column<float>(nullable: false),
                    SkewY = table.Column<float>(nullable: false),
                    GearId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GearImage_Gear_GearId",
                        column: x => x.GearId,
                        principalTable: "Gear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                name: "TopUp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TopUpCardId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    TopUpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopUp_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopUp_TopUpCard_TopUpCardId",
                        column: x => x.TopUpCardId,
                        principalTable: "TopUpCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => new { x.NotificationId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_UserNotification_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotification_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    VenueTypeId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venue_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venue_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Venue_VenueType_VenueTypeId",
                        column: x => x.VenueTypeId,
                        principalTable: "VenueType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    Credits = table.Column<float>(nullable: false),
                    Discount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AverageRating = table.Column<double>(nullable: true),
                    TicketLink = table.Column<string>(nullable: true),
                    VenueId = table.Column<int>(nullable: false),
                    EventTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_EventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueChannel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VenueId = table.Column<int>(nullable: false),
                    ChannelId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueChannel_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenueChannel_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventChannel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    ChannelId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventChannel_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventChannel_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventGear",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    GearId = table.Column<int>(nullable: false),
                    PurchaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGear_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGear_Gear_GearId",
                        column: x => x.GearId,
                        principalTable: "Gear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGear_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventRating",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rate = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRating_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRating_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8a0bedfe-d5c7-40d8-9471-1614f7769cb2", "fc51a9c2-a72e-46b5-8678-2de81cdcfc5f", "Organizer", "ORGANIZER" },
                    { "e80120a3-b085-4f1d-898d-675453fa98e1", "c9e0d98c-7943-48d4-8b00-a48828161b35", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bosnia and Herzegovina" },
                    { 2, "Croatia" },
                    { 3, "Serbia" }
                });

            migrationBuilder.InsertData(
                table: "EventType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Concert" },
                    { 2, "Party" },
                    { 3, "Performance" }
                });

            migrationBuilder.InsertData(
                table: "GearType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Lights" },
                    { 2, "Speakers" },
                    { 1, "Microphones" }
                });

            migrationBuilder.InsertData(
                table: "TopUpCard",
                columns: new[] { "Id", "Amount", "Code" },
                values: new object[,]
                {
                    { 1, 100, "ABCDEFG123456100A" },
                    { 2, 200, "ABCDEFG123456200A" },
                    { 3, 100, "ABCDEFG123456100B" },
                    { 4, 200, "ABCDEFG123456200B" },
                    { 5, 200, "ABCDEFG123456200C" },
                    { 6, 100, "ABCDEFG123456100C" }
                });

            migrationBuilder.InsertData(
                table: "VenueType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Club" },
                    { 2, "Theater" },
                    { 3, "Hall" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Mostar" },
                    { 2, 1, "Sarajevo" },
                    { 3, 2, "Zagreb" },
                    { 4, 3, "Beograd" }
                });

            migrationBuilder.InsertData(
                table: "Gear",
                columns: new[] { "Id", "GearTypeId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Korg KG1", 5f },
                    { 2, 2, "Sony AX15", 10f },
                    { 3, 3, "Lux AL350", 15f }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "CityId", "Code", "Street" },
                values: new object[,]
                {
                    { 1, 1, "88000", "Dubrovacka" },
                    { 2, 2, "71000", "Mehmeda Spahe" },
                    { 3, 3, "10020", "Masiceva" },
                    { 4, 4, "11000", "Savska" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", 0, 1, "ea80dd0f-34f5-441f-a94e-d512ffe95704", "organizer1@gmail.com", false, "Emir", "M", "Memic", false, null, "ORGANIZER1@GMAIL.COM", "ORGANIZER1", "AQAAAAEAACcQAAAAEKDjNbvkdEfkzDmZak9i59Pi5m9xErsn4ONrI0LD+izYDt+gyHFHm6ekiWcGYiyHYw==", null, false, "stamp1", false, "Organizer1" },
                    { "4a74ef28-adb9-4817-8d04-1928dcf3ab32", 0, 2, "286adab1-f53d-4882-af25-7d122c5830b2", "user1@gmail.com", false, "Melisa", "F", "Ibric", false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAEAACcQAAAAEHJ2RKnU9D2CWi0dWq/PiouzkOGsF3eP2liTD5dV9gGLCOpFurmWI38CSuCISscI5g==", null, false, "stamp2", false, "User1" },
                    { "6ac426e9-e07e-434a-885f-ff3e7489f1c4", 0, 3, "fc059bdd-8e2e-469f-a9ff-0e731227f248", "user2@gmail.com", false, "Tarik", "M", "Novalic", false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAEAACcQAAAAEFUclO1t2Bm1slv2dc6m5AkUQxVtekaw+W6ZgY6C7LadSUUEfs+Hl3R4zcg0mKdqNQ==", null, false, "stamp3", false, "User2" },
                    { "9286a727-d145-4e89-b48f-67d760d77855", 0, 4, "e1db69b2-d589-4a36-ba3e-bfe0ff100d77", "user3@gmail.com", false, "Riad", "M", "Sendic", false, null, "USER3@GMAIL.COM", "USER3", "AQAAAAEAACcQAAAAEP6nAWGFwUNT8dUR+PXRTriKVwWTL57R3d6quJgsKDLypJVYWp62sEgqwNgcJbZP3g==", null, false, "stamp4", false, "User3" }
                });

            migrationBuilder.InsertData(
                table: "GearImage",
                columns: new[] { "Id", "ContainerH", "ContainerW", "GearId", "ImageLink", "ScaleX", "ScaleY", "SkewX", "SkewY", "TranslateX", "TranslateY" },
                values: new object[,]
                {
                    { 4, 100f, 100f, 1, "/EventStage/StageEquipment/Microphone1.png", 0.4f, 0.3f, 0f, 0f, 50f, 70f },
                    { 1, 30f, 12f, 2, "/EventStage/StageEquipment/SpeakerAX.png", 1f, 1f, 0f, 0f, 650f, 200f },
                    { 2, 30f, 12f, 2, "/EventStage/StageEquipment/SpeakerAX.png", -1f, 1f, 0f, 0f, -130f, 200f },
                    { 3, 30f, 80f, 3, "/EventStage/StageEquipment/StageLights.png", 0.6f, 0.6f, 0f, 0f, 5f, 0f }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", "8a0bedfe-d5c7-40d8-9471-1614f7769cb2" },
                    { "4a74ef28-adb9-4817-8d04-1928dcf3ab32", "e80120a3-b085-4f1d-898d-675453fa98e1" },
                    { "6ac426e9-e07e-434a-885f-ff3e7489f1c4", "e80120a3-b085-4f1d-898d-675453fa98e1" },
                    { "9286a727-d145-4e89-b48f-67d760d77855", "e80120a3-b085-4f1d-898d-675453fa98e1" }
                });

            migrationBuilder.InsertData(
                table: "TopUp",
                columns: new[] { "Id", "AppUserId", "TopUpCardId", "TopUpDate" },
                values: new object[,]
                {
                    { 1, "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", 1, new DateTime(2021, 7, 21, 0, 54, 13, 603, DateTimeKind.Local) },
                    { 2, "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", 2, new DateTime(2021, 7, 21, 0, 54, 13, 604, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Venue",
                columns: new[] { "Id", "AddressId", "AppUserId", "Capacity", "ImageUrl", "Name", "VenueTypeId" },
                values: new object[,]
                {
                    { 1, 1, "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", 1500, "Venue1.jpg", "Parallax", 1 },
                    { 2, 2, "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", 1000, "Venue2.jpg", "Obsidian", 2 },
                    { 3, 3, "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", 500, "Venue3.jpg", "Galaxis", 3 }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "AppUserId", "Credits", "Discount" },
                values: new object[] { 1, "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb", 500f, 4 });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "AverageRating", "CreatedBy", "CreatedDate", "Description", "EndDate", "EventTypeId", "ImageUrl", "Name", "StartDate", "TicketLink", "UpdatedDate", "VenueId" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2021, 7, 21, 0, 54, 13, 604, DateTimeKind.Local), "Dino is recognised for his later solo work during which he established himself as one of the best-selling regional artists of all time. During his career he has produced over a dozen chart-topping albums. Don't miss your opportunity to attend.", new DateTime(2021, 8, 5, 23, 0, 0, 0, DateTimeKind.Unspecified), 1, "DinoMerlin1.jpg", "Dino Merlin", new DateTime(2021, 8, 5, 20, 30, 0, 0, DateTimeKind.Unspecified), null, null, 1 },
                    { 4, null, null, new DateTime(2021, 7, 21, 0, 54, 13, 604, DateTimeKind.Local), "Dino is recognised for his later solo work during which he established himself as one of the best-selling regional artists of all time. During his career he has produced over a dozen chart-topping albums. Don't miss your opportunity to attend.", new DateTime(2021, 9, 4, 22, 0, 0, 0, DateTimeKind.Unspecified), 1, "DinoMerlin2.jpg", "Dino Merlin 2", new DateTime(2021, 9, 4, 19, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1 },
                    { 2, null, null, new DateTime(2021, 7, 21, 0, 54, 13, 604, DateTimeKind.Local), "Eddie was an American guitarist and singer in early funk music who played lead guitar with Parliament-Funkadelic. His ten-minute guitar solo in the Funkadelic song Maggot Brain is hailed as one of the greatest solos of all time on any instrument", new DateTime(2021, 9, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, "EddieHazel1.jpg", "Eddie Hazel", new DateTime(2021, 9, 10, 11, 30, 0, 0, DateTimeKind.Unspecified), null, null, 2 },
                    { 5, null, null, new DateTime(2021, 7, 21, 0, 54, 13, 604, DateTimeKind.Local), "Jimi was an American musician, singer, and songwriter. Although his mainstream career spanned only four years, he is widely regarded as one of the most influential electric guitarists in the history of popular music, and one of the most celebrated musicians of the 20th century. ", new DateTime(2021, 10, 5, 22, 30, 0, 0, DateTimeKind.Unspecified), 3, "JimiHendrix1.jpg", "Jimi Hendrix", new DateTime(2021, 10, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2 },
                    { 3, null, null, new DateTime(2021, 7, 21, 0, 54, 13, 604, DateTimeKind.Local), "Best duck party around, you will regret if you don't come and will have to wait forever for another one. So don't miss out!", new DateTime(2021, 11, 16, 23, 0, 0, 0, DateTimeKind.Unspecified), 2, "DuckParty1.jpg", "Duck Party", new DateTime(2021, 11, 16, 20, 30, 0, 0, DateTimeKind.Unspecified), null, null, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_EventId",
                table: "Comment",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventTypeId",
                table: "Event",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_VenueId",
                table: "Event",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_EventChannel_ChannelId",
                table: "EventChannel",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_EventChannel_EventId",
                table: "EventChannel",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGear_EventId",
                table: "EventGear",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGear_GearId",
                table: "EventGear",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGear_PurchaseId",
                table: "EventGear",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRating_EventId",
                table: "EventRating",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRating_UserId",
                table: "EventRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gear_GearTypeId",
                table: "Gear",
                column: "GearTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GearImage_GearId",
                table: "GearImage",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_TopUp_AppUserId",
                table: "TopUp",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TopUp_TopUpCardId",
                table: "TopUp",
                column: "TopUpCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_AppUserId",
                table: "UserNotification",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Venue_AddressId",
                table: "Venue",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Venue_AppUserId",
                table: "Venue",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Venue_VenueTypeId",
                table: "Venue",
                column: "VenueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueChannel_ChannelId",
                table: "VenueChannel",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueChannel_VenueId",
                table: "VenueChannel",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_AppUserId",
                table: "Wallet",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Comment");

            migrationBuilder.DropTable(
                name: "EventChannel");

            migrationBuilder.DropTable(
                name: "EventGear");

            migrationBuilder.DropTable(
                name: "EventRating");

            migrationBuilder.DropTable(
                name: "ExceptionLog");

            migrationBuilder.DropTable(
                name: "GearImage");

            migrationBuilder.DropTable(
                name: "TopUp");

            migrationBuilder.DropTable(
                name: "UserNotification");

            migrationBuilder.DropTable(
                name: "VenueChannel");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Gear");

            migrationBuilder.DropTable(
                name: "TopUpCard");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "Venue");

            migrationBuilder.DropTable(
                name: "GearType");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "VenueType");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
