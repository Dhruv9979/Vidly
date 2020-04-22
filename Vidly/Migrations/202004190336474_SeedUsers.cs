namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ce3075d7-5d1e-4426-8dd3-653eed953f7f', N'admin@vidly.com', 0, N'AKON2AvhmAIyiTY8jPQTq0jZ/8MBBkuegq4M51nBhuYFo4cRznVTHaPgezfPYNDvlg==', N'1475343e-25b5-4b3b-a057-28eaaa868e56', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd8997be6-ceb3-44f1-86a6-734102b21b72', N'guest@vidly.com', 0, N'ABdOlAOMisYbJAlJTt9EPtKgAxJ9Pv9R0OU0Ec2YVNTC/8sqKsRbjl9rw6RSJQKWcw==', N'4a90b7d8-533d-41d3-ace0-890181b2433c', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'cbf3df96-cdc2-4ca6-a4dc-7c96dfe94b12', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ce3075d7-5d1e-4426-8dd3-653eed953f7f', N'cbf3df96-cdc2-4ca6-a4dc-7c96dfe94b12')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
