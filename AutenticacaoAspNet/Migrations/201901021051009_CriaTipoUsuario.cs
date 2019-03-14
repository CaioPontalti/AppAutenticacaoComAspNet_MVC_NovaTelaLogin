namespace AutenticacaoAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaTipoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "TipoUsuario", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "TipoUsuario");
        }
    }
}
