using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFramework.SqlServer;
namespace WebApiPractica.Models

public class equiposContext : DbContext
{
	public equiposContext(DbContextequipos<equiposContext> options) : base(options) 
	{ 

	}
}
