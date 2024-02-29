using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineTicketsReservation.Migrations
{
    public partial class Migrimm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aeroplanet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nr_Uleseve_VIP = table.Column<int>(type: "int", nullable: false),
                    Nr_Uleseve_Biznes = table.Column<int>(type: "int", nullable: false),
                    Nr_Uleseve_Ekonomike = table.Column<int>(type: "int", nullable: false),
                    Kompania = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroplanet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kontakti",
                columns: table => new
                {
                    KontaktiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefoni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mesazhi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakti", x => x.KontaktiID);
                });

            migrationBuilder.CreateTable(
                name: "Shteti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shteti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vetura",
                columns: table => new
                {
                    VeturaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VitiProdhimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Karburanti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cmimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PershkrimiModelit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vetura", x => x.VeturaID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Qyteti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShtetiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qyteti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qyteti_Shteti_ShtetiId",
                        column: x => x.ShtetiId,
                        principalTable: "Shteti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aeroporti",
                columns: table => new
                {
                    AeroportiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QytetiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroporti", x => x.AeroportiID);
                    table.ForeignKey(
                        name: "FK_Aeroporti_Qyteti_QytetiID",
                        column: x => x.QytetiID,
                        principalTable: "Qyteti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fluturimet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrFluturimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeparuteAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KohaENisjes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KohaEArritjes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cmimi = table.Column<int>(type: "int", nullable: false),
                    AeroplaniId = table.Column<int>(type: "int", nullable: false),
                    QytetiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluturimet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fluturimet_Aeroplanet_AeroplaniId",
                        column: x => x.AeroplaniId,
                        principalTable: "Aeroplanet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fluturimet_Qyteti_QytetiId",
                        column: x => x.QytetiId,
                        principalTable: "Qyteti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hoteli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pershkrimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QytetiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hoteli_Qyteti_QytetiId",
                        column: x => x.QytetiId,
                        principalTable: "Qyteti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervimiV",
                columns: table => new
                {
                    RezervimiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFillimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataKthimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AeroportiID = table.Column<int>(type: "int", nullable: false),
                    VeturaID = table.Column<int>(type: "int", nullable: false),
                    Cmimi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervimiV", x => x.RezervimiID);
                    table.ForeignKey(
                        name: "FK_RezervimiV_Aeroporti_AeroportiID",
                        column: x => x.AeroportiID,
                        principalTable: "Aeroporti",
                        principalColumn: "AeroportiID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervimiV_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RezervimiV_Vetura_VeturaID",
                        column: x => x.VeturaID,
                        principalTable: "Vetura",
                        principalColumn: "VeturaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rezervimet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriPasagjerit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MbiemriPasagjerit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Klasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cmimi = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FluturimiId = table.Column<int>(type: "int", nullable: false),
                    Kthyese = table.Column<bool>(type: "bit", nullable: false),
                    Data_e_Rezervimit = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Data_e_Kthimit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervimet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervimet_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rezervimet_Fluturimet_FluturimiId",
                        column: x => x.FluturimiId,
                        principalTable: "Fluturimet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ofertat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cmimi = table.Column<int>(type: "int", nullable: false),
                    HoteliId = table.Column<int>(type: "int", nullable: false),
                    FluturimiId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertat_Fluturimet_FluturimiId",
                        column: x => x.FluturimiId,
                        principalTable: "Fluturimet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ofertat_Hoteli_HoteliId",
                        column: x => x.HoteliId,
                        principalTable: "Hoteli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rezervimet_me_Oferte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_E_Rezervimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_E_Kthimit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cmimi = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfertaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervimet_me_Oferte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervimet_me_Oferte_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rezervimet_me_Oferte_Ofertat_OfertaId",
                        column: x => x.OfertaId,
                        principalTable: "Ofertat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a98fd2f-687a-4c4e-93da-b787a8c0ff05", "1a98fd2f-687a-4c4e-93da-b787a8c0ff05", "User", "User" },
                    { "5811453b-6a15-4bd0-82ca-fc98643dfb3b", "5811453b-6a15-4bd0-82ca-fc98643dfb3b", "Admin", "Admin" },
                    { "8518f0ce-cf65-4517-a2b9-2d8f50f1d5cd", "8518f0ce-cf65-4517-a2b9-2d8f50f1d5cd", "SuperAdmin", "SuperAdmin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "823ff811-fe7b-4b83-84e2-330ba8905a23", 0, "14fcad71-4835-49b6-95f1-e78a9447635c", "superadmin@gmail.com", false, false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEA/UiglwiY8qq5PWpJ7mHZmJoSMxiRjV6WRSfCVbW3v52VLvXdm7l90DjsiFeoKDMA==", null, false, "7f874e04-09a2-4de0-b068-e1b23283a51c", false, "superadmin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1a98fd2f-687a-4c4e-93da-b787a8c0ff05", "823ff811-fe7b-4b83-84e2-330ba8905a23" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5811453b-6a15-4bd0-82ca-fc98643dfb3b", "823ff811-fe7b-4b83-84e2-330ba8905a23" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8518f0ce-cf65-4517-a2b9-2d8f50f1d5cd", "823ff811-fe7b-4b83-84e2-330ba8905a23" });

            migrationBuilder.CreateIndex(
                name: "IX_Aeroporti_QytetiID",
                table: "Aeroporti",
                column: "QytetiID",
                unique: true);

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
                name: "IX_Fluturimet_AeroplaniId",
                table: "Fluturimet",
                column: "AeroplaniId");

            migrationBuilder.CreateIndex(
                name: "IX_Fluturimet_QytetiId",
                table: "Fluturimet",
                column: "QytetiId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteli_QytetiId",
                table: "Hoteli",
                column: "QytetiId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertat_FluturimiId",
                table: "Ofertat",
                column: "FluturimiId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertat_HoteliId",
                table: "Ofertat",
                column: "HoteliId");

            migrationBuilder.CreateIndex(
                name: "IX_Qyteti_ShtetiId",
                table: "Qyteti",
                column: "ShtetiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervimet_FluturimiId",
                table: "Rezervimet",
                column: "FluturimiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervimet_UserId",
                table: "Rezervimet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervimet_me_Oferte_OfertaId",
                table: "Rezervimet_me_Oferte",
                column: "OfertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervimet_me_Oferte_UserId",
                table: "Rezervimet_me_Oferte",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervimiV_AeroportiID",
                table: "RezervimiV",
                column: "AeroportiID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervimiV_UserId",
                table: "RezervimiV",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervimiV_VeturaID",
                table: "RezervimiV",
                column: "VeturaID");
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
                name: "Kontakti");

            migrationBuilder.DropTable(
                name: "Rezervimet");

            migrationBuilder.DropTable(
                name: "Rezervimet_me_Oferte");

            migrationBuilder.DropTable(
                name: "RezervimiV");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Ofertat");

            migrationBuilder.DropTable(
                name: "Aeroporti");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Vetura");

            migrationBuilder.DropTable(
                name: "Fluturimet");

            migrationBuilder.DropTable(
                name: "Hoteli");

            migrationBuilder.DropTable(
                name: "Aeroplanet");

            migrationBuilder.DropTable(
                name: "Qyteti");

            migrationBuilder.DropTable(
                name: "Shteti");
        }
    }
}
