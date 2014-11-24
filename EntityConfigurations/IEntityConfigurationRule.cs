using System.Data.Entity.ModelConfiguration;

namespace MFramework.Domain.Services.Repository.EF.EntityConfigurations
{
 /// <summary>
 /// Convenzione  per la configurazione  degli EntityModelConfiguration
 /// </summary>
 /// <typeparam name="T">Entity Type</typeparam>
    public interface IEntityConfigurationRule<T>
        where T:class
 {
     /// <summary>
     /// restituisce il nome della tabella a cui deve essere mappata l'entita
     /// </summary>
     /// <param name="fortype"></param>
     /// <returns></returns>
     string TableName { get; }
     /// <summary>
     /// Specifica il nome della tabella a cui mappare l'enitita
     /// </summary>
     /// <param name="cfg"></param>
        void ToTable(EntityTypeConfiguration<T> cfg);

     
 }
    
}
