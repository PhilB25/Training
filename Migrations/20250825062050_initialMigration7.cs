using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AT_API.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string[] text = { "AAA", "BBB", "CCC", "DDD", "EEE", "FFF", "GGG", "HHH", "III", "JJJ" }; 
            Random r = new Random();
            var c = 1;
            foreach (string value in text)
            {
                int authorId = r.Next(1, 6);
                string[] strings = { "D6D1E4A1-F45D-4610-BD6A-39BD752296BB", "2E0892C2-AD5F-44FE-BD84-943077226487", "47467807-6A97-4CCC-8F4E-BDB9D98D0A09", "86199205-ED57-4B7A-B86D-C5898F265868", "40820257-C03A-4483-9D76-D702144BCC7E" };
                Guid x = Guid.Parse(strings[authorId-1]);
                migrationBuilder.Sql($"insert into courses (title, description, authorid) values ('{value}', '{value}' ,'{x}' )");
                c++;
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
