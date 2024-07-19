using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esbas_internship_backendproject.Migrations
{
    public partial class AddStatusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event_Type",
                columns: table => new
                {
                    Event_TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event_Type_Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Type", x => x.Event_TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Event_Location",
                columns: table => new
                {
                    Event_LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event_Location_Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Location", x => x.Event_LocationID);
                });

            migrationBuilder.CreateTable(
                name: "User_Department",
                columns: table => new
                {
                    User_DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Department_Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Department", x => x.User_DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "User_IsOfficeEmployee",
                columns: table => new
                {
                    User_IsOfficeEmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_IsOfficeEmployee_Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_IsOfficeEmployee", x => x.User_IsOfficeEmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "User_Gender",
                columns: table => new
                {
                    User_GenderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Gender_Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Gender", x => x.User_GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Event_TypeID = table.Column<int>(type: "int", nullable: false),
                    Event_LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Event_Type_Event_TypeID",
                        column: x => x.Event_TypeID,
                        principalTable: "Event_Type",
                        principalColumn: "Event_TypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Event_Location_Event_LocationID",
                        column: x => x.Event_LocationID,
                        principalTable: "Event_Location",
                        principalColumn: "Event_LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsOfficeEmployee = table.Column<bool>(type: "bit", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    User_DepartmentID = table.Column<int>(type: "int", nullable: false),
                    User_IsOfficeEmployeeID = table.Column<int>(type: "int", nullable: false),
                    User_GenderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_User_Department_User_DepartmentID",
                        column: x => x.User_DepartmentID,
                        principalTable: "User_Department",
                        principalColumn: "User_DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_User_IsOfficeEmployee_User_IsOfficeEmployeeID",
                        column: x => x.User_IsOfficeEmployeeID,
                        principalTable: "User_IsOfficeEmployee",
                        principalColumn: "User_IsOfficeEmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_User_Gender_User_GenderID",
                        column: x => x.User_GenderID,
                        principalTable: "User_Gender",
                        principalColumn: "User_GenderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events_Users",
                columns: table => new
                {
                    Events_UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events_Users", x => x.Events_UserID);
                    table.ForeignKey(
                        name: "FK_Events_Users_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Users_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Event_TypeID",
                table: "Events",
                column: "Event_TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Event_LocationID",
                table: "Events",
                column: "Event_LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_User_DepartmentID",
                table: "Users",
                column: "User_DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_User_IsOfficeEmployeeID",
                table: "Users",
                column: "User_IsOfficeEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_User_GenderID",
                table: "Users",
                column: "User_GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Users_EventID",
                table: "Events_Users",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Users_UserID",
                table: "Events_Users",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events_Users");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Event_Type");

            migrationBuilder.DropTable(
                name: "Event_Location");

            migrationBuilder.DropTable(
                name: "User_Department");

            migrationBuilder.DropTable(
                name: "User_IsOfficeEmployee");

            migrationBuilder.DropTable(
                name: "User_Gender");
        }
    }
}
