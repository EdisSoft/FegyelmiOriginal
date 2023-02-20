using EntityFramework.DynamicFilters;
using Edis.Diagnostics;
using Edis.Entities.Attributes;
using Edis.Entities.Base;
using Edis.Entities.Common;
using Edis.Entities.Fany;
using Edis.Entities.JFK.FENY;
using Edis.Entities.JFK.Jutalmazas;
using Edis.IoC;
using Edis.Repositories.Utils;
using Edis.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
//using Edis.Entities.Fonix3;

namespace Edis.Repositories.Contexts
{

    public class KonasoftBVFonixContext : DbContext
    {
        public static ConcurrentDictionary<HttpContext, KonasoftBVFonixContext> GlobalContexts { get; set; }

        static KonasoftBVFonixContext()
        {
            Database.SetInitializer<KonasoftBVFonixContext>(null);
            GlobalContexts = new ConcurrentDictionary<HttpContext, KonasoftBVFonixContext>();
        }

        public static KonasoftBVFonixContext GetContextInstance()
        {
            var key = HttpContext.Current;
            KonasoftBVFonixContext context = null;
            if (key == null) // ha nincs http context, nem web request
                return new KonasoftBVFonixContext();
            if (!GlobalContexts.ContainsKey(key))
            {
                context = new KonasoftBVFonixContext();
                GlobalContexts.TryAdd(key, context);
            }
            else
            {
                context = GlobalContexts[key];

                try
                {
                    if (context.IsDisposed())
                        context = GlobalContexts[key] = new KonasoftBVFonixContext();
                }
                catch { }

            }
            return context;
        }

        public static void DeleteContext()
        {
            var key = HttpContext.Current;
            if (GlobalContexts.ContainsKey(key))
            {
                KonasoftBVFonixContext val = null;
                GlobalContexts.TryRemove(key, out val);
                if (val != null) val.Dispose();
            }
        }

        public KonasoftBVFonixContext()
        {

        }

        public List<T> RunStoredProcedureByNev<T>(string storedProcedureNev, Dictionary<string, object> parameters)
        {
            try
            {
                string paramlist = "";
                bool withparameters = false;
                if (parameters != null && parameters.Count > 0)
                {
                    paramlist = string.Join(", ", parameters.Keys.ToList());
                    withparameters = true;
                }
                List<T> result;
                this.Database.CommandTimeout = 0;
                var start = DateTime.Now;
                if (withparameters)
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    foreach (var key in parameters.Keys)
                    {
                        sqlParams.Add(new SqlParameter(key, parameters[key] == null ? DBNull.Value : parameters[key]));
                    }
                    result =
                        this.Database.SqlQuery<T>
                        ($"EXEC {storedProcedureNev} {paramlist}", sqlParams.ToArray()).ToList();
                }
                else
                {
                    result =
                        this.Database.SqlQuery<T>
                        ($"EXEC {storedProcedureNev}").ToList();

                }
                var end = DateTime.Now;
                if (end - start > TimeSpan.FromSeconds(15)) Log.Debug($"{storedProcedureNev} futása hosszú volt: {(end - start).TotalMilliseconds}ms");
                return result;

            }
            catch (Exception exc)
            {
                Log.Error($"RunStoredProcedureByNev Nev: {storedProcedureNev}", exc);
                throw exc;
            }

        }
        public void ExecuteSqlCommandByNev(string storedProcedureNev, Dictionary<string, object> parameters)
        {
            try
            {
                string paramlist = "";
                bool withparameters = false;
                if (parameters != null && parameters.Count > 0)
                {
                    paramlist = string.Join(", ", parameters.Keys.ToList());
                    withparameters = true;
                }
                this.Database.CommandTimeout = 0;
                var start = DateTime.Now;
                if (withparameters)
                {
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    foreach (var key in parameters.Keys)
                    {
                        sqlParams.Add(new SqlParameter(key, parameters[key] == null ? DBNull.Value : parameters[key]));
                    }
                    this.Database.ExecuteSqlCommand
                    ($"EXEC {storedProcedureNev} {paramlist}", sqlParams.ToArray());
                }
                else
                {
                    this.Database.ExecuteSqlCommand
                    ($"EXEC {storedProcedureNev}");
                }
                var end = DateTime.Now;
                if (end - start > TimeSpan.FromSeconds(15)) Log.Debug($"{storedProcedureNev} futása hosszú volt: {(end - start).TotalMilliseconds}ms");

            }
            catch (Exception exc)
            {
                Log.Error($"RunVoidStoredProcedureByNev Nev: {storedProcedureNev}", exc);
                throw exc;
            }

        }

        public void ExecuteSqlCommandByNev(string storedProcedureNev, List<SqlParameter> parameters)
        {
            try
            {
                this.Database.CommandTimeout = 0;
                var start = DateTime.Now;

                if (parameters != null && parameters.Any())
                {
                    var paramlist = string.Join(", ", parameters.Select(s => s.ParameterName).ToList());

                    this.Database.ExecuteSqlCommand
                    ($"EXEC {storedProcedureNev} {paramlist}", parameters.ToArray());
                }
                else
                {
                    this.Database.ExecuteSqlCommand
                    ($"EXEC {storedProcedureNev}");
                }

                var end = DateTime.Now;
                if (end - start > TimeSpan.FromSeconds(15)) Log.Debug($"{storedProcedureNev} futása hosszú volt: {(end - start).TotalMilliseconds}ms");

            }
            catch (Exception exc)
            {
                Log.Error($"RunVoidStoredProcedureByNev v2 Nev: {storedProcedureNev}", exc);
                throw exc;
            }

        }


        public static string ClearOldContexts(int tresholdInMinute)
        {
            string res = GlobalContexts.Count + " Contextből ";
            int cleared = 0;

            var keysToClear = GlobalContexts.Keys.Where(k => k.Timestamp <= DateTime.Now.AddMinutes(-1 * tresholdInMinute));

            res += tresholdInMinute + " percnél régebbiek száma: " + keysToClear.Count();
            foreach (var item in keysToClear)
            {
                KonasoftBVFonixContext val = null;
                if (GlobalContexts.TryRemove(item, out val))
                {
                    if (val != null) val.Dispose();
                    cleared++;
                }
            }
            res += ", ebből törölve: " + cleared;

            return res;
        }

        #region DBSets

        #region Common
        public DbSet<SajatLink> SajatLink { get; set; }
        public DbSet<LoginBeallitasok> LoginBeallitasok { get; set; }
        public DbSet<NevelesiCsoport> NevelesiCsoportok { get; set; }
        public DbSet<FunkcioEngedelyezes> FunkcioEngedelyezes { get; set; }
        public DbSet<Cimke> Cimkek { get; set; }
        public DbSet<Felho> Felhok { get; set; }
        public DbSet<ManipulationActiveDirectory> ManipulationActiveDirectory { get; set; }

        #endregion

        #region FANY

        public DbSet<Fogvatartott> Fogvatartottak { get; set; }
        //public DbSet<BvBankFogvatartot_t> BvBankFogvatartot_tak { get; set; }
        public DbSet<FogvatartottNezet> FogvatartottakNezet { get; set; }
        public DbSet<FogvatartottSzemelyesAdatai> FogvatartottSzemelyesAdatai { get; set; }
        public DbSet<Szemelyzet> Szemelyzet { get; set; }
        public DbSet<SzemelyzetSzerepkor> SzemelyzetSzerepkor { get; set; }
        public DbSet<SzemelyzetCsoport> SzemelyzetCsoport { get; set; }
        public DbSet<SzemelyzetJogosultsag> SzemelyzetJogosultsag { get; set; }
        public DbSet<Intezet> Intezet { get; set; }
        public DbSet<Kodszotar> Kodszotar { get; set; }
        public DbSet<KodszotarCsoport> KodszotarCsoport { get; set; }
        public DbSet<Helyseg> Helyseg { get; set; }
        public DbSet<IntezetiObjektum> IntezetiObjektum { get; set; }
        public DbSet<Korlet> Korlet { get; set; }
        public DbSet<Zarka> Zarka { get; set; }

        #endregion

       

       

        public DbSet<NyomtatvanySablon> NyomtatvanySablon { get; set; }


      
      
        #region JFK
        public DbSet<IktatottNyomtatvanyok> IktatottNyomtatvanyok { get; set; }
        public DbSet<IktatottDokumentumok> IktatottDokumentumok { get; set; }
        public DbSet<Esemenyek> Esemenyek { get; set; }
        public DbSet<FogvatartottListaItemFegyelmiView> FogvatartottakFegyelmiView { get; set; }

        public DbSet<EsemenyResztvevo> EsemenyResztvevok { get; set; }

        public DbSet<FegyelmiUgy> FegyelmiUgyek { get; set; }
        public DbSet<Naplo> Naplo { get; set; }
        public DbSet<Feltoltesek> Feltoltesek { get; set; }
        public DbSet<Felfuggesztes> Felfuggesztesek { get; set; }
        public DbSet<SzakteruletiVelemenyKeresek> SzakteruletiVelemenyKeresek { get; set; }
        public DbSet<FenyitesVegrehajtasok> FenyitesVegrehajtasok { get; set; }
        public DbSet<VegrehajtoSzakteruletek> VegrehajtoSzakteruletek { get; set; }
        public DbSet<EmailErtesites> EmailErtesitesek { get; set; }
        public DbSet<MaganelzarasFofelugyelok> MaganelzarasFofelugyelok { get; set; }


        #endregion

        #endregion





        public override int SaveChanges()
        {

            this.ChangeTracker.DetectChanges();
            var changedEntries = this.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            if (changedEntries.Count == 0) return 0;

            var rogzitoIntezetId = int.Parse(ConfigurationManager.AppSettings["rogzitoIntezetId"] ?? "100");
            var rogzitoSzemelyId = int.Parse(ConfigurationManager.AppSettings["rogzitoSzemelyId"] ?? "100");

            foreach (DbEntityEntry entry in changedEntries)
            {
                UpperCaseFirstAllStringProperty(entry);
                ToTitleCaseAllStringProperty(entry);
            }


            foreach (DbEntityEntry entry in changedEntries.Where(x => x.Entity.GetType().IsSubclassOf(typeof(KonaExtendedEntity))))
            {
                KonaExtendedEntity entity = (KonaExtendedEntity)entry.Entity;
                if (!entity.KeziRogzitoAdatok)
                {
                    ITranzakcioAdatKontextusFunctions appsettingsFunctions = new TranzakcioAdatKontextusFunctions();

                    if (HttpContext.Current == null)
                    {
                        entity.RogzitoIntezetId = rogzitoIntezetId;
                        entity.RogzitoSzemelyId = rogzitoSzemelyId;
                    }
                    else
                    {
                        entity.RogzitoIntezetId = appsettingsFunctions.Kontextus.RogzitoIntezetId;
                        entity.RogzitoSzemelyId = appsettingsFunctions.Kontextus.SzemelyzetId;
                    }
                    if (entity.RogzitoSzemelyId == 0)
                        if (ConfigurationManager.AppSettings["ServiceRogzitoSzemelyId"] == null)
                            throw new Exception($"Rögzítő személy id nem lehet 0, adat mentésekor! Kontextus: {appsettingsFunctions.Kontextus?.ToString() ?? "null"}");
                        else
                            entity.RogzitoSzemelyId = int.Parse(ConfigurationManager.AppSettings["ServiceRogzitoSzemelyId"]);

                    if (entity.RogzitoIntezetId == 0)
                        if (ConfigurationManager.AppSettings["ServiceRogzitoIntezetId"] == null)
                            throw new Exception($"Rögzítő intézet id nem lehet 0, adat mentésekor! Kontextus: {appsettingsFunctions.Kontextus?.ToString() ?? "null"}");
                        else
                            entity.RogzitoIntezetId = int.Parse(ConfigurationManager.AppSettings["ServiceRogzitoIntezetId"]);
                }
                else
                {
                    if (entity.RogzitoSzemelyId == 0)
                        if (ConfigurationManager.AppSettings["ServiceRogzitoSzemelyId"] == null)
                            throw new Exception($"Rögzítő személy id nem lehet 0, adat mentésekor!");
                        else
                            entity.RogzitoSzemelyId = int.Parse(ConfigurationManager.AppSettings["ServiceRogzitoSzemelyId"]);
                    if (entity.RogzitoIntezetId == 0)
                        if (ConfigurationManager.AppSettings["ServiceRogzitoIntezetId"] == null)
                            throw new Exception($"Rögzítő intézet id nem lehet 0, adat mentésekor!");
                        else
                            entity.RogzitoIntezetId = int.Parse(ConfigurationManager.AppSettings["ServiceRogzitoIntezetId"]);
                }

                if (entity is KeziJavitoEntity)
                {
                    KeziJavitoEntity entityBvBank = (KeziJavitoEntity)entity;
                    if (entry.State == EntityState.Added)
                        entityBvBank.LetrehozasDatuma = DateTime.Now;
                    entityBvBank.KeziJavitasAzonosito = null;

                }

                entity.ErvenyessegKezdete = DateTime.Now;
                switch (entry.State)
                {
                    //case EntityState.Added:
                    //    entity.OnCreate(tranzakcio.Id);
                    //    break;
                    //case EntityState.Modified:
                    //    entity.OnModify(tranzakcio.Id);
                    //    break;
                    case EntityState.Deleted:
                        entity.TOROLT_FL = true;
                        Entry(entity).State = EntityState.Modified;
                        break;
                }
            }

            foreach (DbEntityEntry entry in changedEntries.Where(x => x.Entity.GetType().IsSubclassOf(typeof(NotFilteredBaseEntity))))
            {
                NotFilteredBaseEntity entity = (NotFilteredBaseEntity)entry.Entity;
                ITranzakcioAdatKontextusFunctions appsettingsFunctions = new TranzakcioAdatKontextusFunctions();

                entity.RogzitoIntezetId = appsettingsFunctions.Kontextus.RogzitoIntezetId;
                entity.RogzitoSzemelyId = appsettingsFunctions.Kontextus.SzemelyzetId;
                if (entity.RogzitoSzemelyId == 0)
                    if (ConfigurationManager.AppSettings["ServiceRogzitoSzemelyId"] == null)
                        throw new Exception($"Rögzítő személy id nem lehet 0, adat mentésekor! Kontextus: {appsettingsFunctions.Kontextus?.ToString() ?? "null"}");
                    else
                        entity.RogzitoSzemelyId = int.Parse(ConfigurationManager.AppSettings["ServiceRogzitoSzemelyId"]);

                if (entity.RogzitoIntezetId == 0)
                    if (ConfigurationManager.AppSettings["ServiceRogzitoIntezetId"] == null)
                        throw new Exception($"Rögzítő intézet id nem lehet 0, adat mentésekor! Kontextus: {appsettingsFunctions.Kontextus?.ToString() ?? "null"}");
                    else
                        entity.RogzitoIntezetId = int.Parse(ConfigurationManager.AppSettings["ServiceRogzitoIntezetId"]);
                entity.ErvenyessegKezdete = DateTime.Now;
                switch (entry.State)
                {
                    //case EntityState.Added:
                    //    entity.OnCreate(tranzakcio.Id);
                    //    break;
                    //case EntityState.Modified:
                    //    entity.OnModify(tranzakcio.Id);
                    //    break;
                    case EntityState.Deleted:
                        entity.TOROLT_FL = true;
                        Entry(entity).State = EntityState.Modified;
                        break;
                }
            }

            foreach (DbEntityEntry entry in changedEntries.Where(x => x.Entity is Szemelyzet))
            {
                //   Tranzakcio tranzakcio = CreateTranzakcio();
                Szemelyzet entity = (Szemelyzet)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.OnCreate(44);
                        break;
                    case EntityState.Modified:
                        entity.OnModify(44);
                        break;
                    case EntityState.Deleted:
                        entity.TOROLT_FL = true;
                        entity.OnModify(44);
                        Entry(entity).State = EntityState.Modified;

                        break;
                }
            }

            var timeout = base.Database.CommandTimeout;
            base.Database.CommandTimeout = 300;
            int result = base.SaveChanges();
            base.Database.CommandTimeout = timeout;

            return result;
        }

        public int BaseSaveChanges()
        {
            if (HttpContext.Current != null)
            {
                throw new Exception("Web requestből tilos ezt a mentést használni, a módosításról nem kerül semmi automatikus bejegyzésre! Csak háttérfolyamatban használható.");
            }
            int result = base.SaveChanges();
            return result;
        }

        private void ToTitleCaseAllStringProperty(DbEntityEntry entry)
        {
            if (entry.Entity == null)
                return;

            var modelType = entry.Entity.GetType();
            var properties = modelType.GetProperties(BindingFlags.NonPublic |
                                                     BindingFlags.Public |
                                                     BindingFlags.Instance);

            foreach (var modelProperty in properties)
            {
                var isToTitleCaseProperty = modelProperty.GetCustomAttributesData().Where(w => w.AttributeType == typeof(ToTitleCaseConversion)).FirstOrDefault() != null;

                if (modelProperty.PropertyType == typeof(String) && isToTitleCaseProperty && modelProperty.CanWrite && modelProperty.GetValue(entry.Entity) != null)
                {
                    var text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(((String)modelProperty.GetValue(entry.Entity)));
                    modelProperty.SetValue(entry.Entity, text);
                }
            }
        }

        private void UpperCaseFirstAllStringProperty(DbEntityEntry entry)
        {
            if (entry.Entity == null)
                return;

            var modelType = entry.Entity.GetType();
            var properties = modelType.GetProperties(BindingFlags.NonPublic |
                                                     BindingFlags.Public |
                                                     BindingFlags.Instance);

            foreach (var modelProperty in properties)
            {
                var isSkipProperty = modelProperty.GetCustomAttributesData().Where(w => w.AttributeType == typeof(SkipFirstUpperCaseConversion)).FirstOrDefault() != null;

                if (modelProperty.PropertyType == typeof(String) && !isSkipProperty && modelProperty.CanWrite)
                {
                    var upperCaseFirstText = ((String)modelProperty.GetValue(entry.Entity)).UppercaseFirst();
                    modelProperty.SetValue(entry.Entity, upperCaseFirstText);
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 4));
            modelBuilder.Properties<decimal?>().Configure(c => c.HasPrecision(18, 4));

            base.OnModelCreating(modelBuilder);

            //MethodInfo method = this.GetType().GetMethod("RegisterSoftDelete", BindingFlags.Instance | BindingFlags.NonPublic);

            //var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            //List<Type> types = new List<Type>();
            //foreach (var item in loadedAssemblies)
            //{
            //    types.AddRange(item.GetTypes());
            //}

            //var baseEntities = types.Where(x => x.IsSubclassOf(typeof(ExtendedBaseEntity))).ToList();


            //foreach (Type type in baseEntities)
            //{   
            //    method.MakeGenericMethod(type ).Invoke(this, new[] { (object)modelBuilder });
            //}

            CreateFiltersOnAllEntities(modelBuilder);

            //modelBuilder.Entity<Fogvatartott>().HasOptional<FogvatartottFenykep>(x => x.FogvatartottFenykep).WithRequired(x => x.Fogvatartott);

            modelBuilder.Entity<Fogvatartott>()
                         .HasMany<FogvatartottFenykep>(s => s.FogvatartottFenykep)
                         .WithRequired(s => s.Fogvatartott)
                         .HasForeignKey(s => s.FogvatartottId);

            modelBuilder.Entity<FogvatartottNezet>()
                         .HasMany<FogvatartottFenykep>(s => s.FogvatartottFenykepek)
                         .WithRequired(s => s.FogvatartottNezet)
                         .HasForeignKey(s => s.FogvatartottId);

            modelBuilder.Entity<Fogvatartott>()
                         .HasMany<FogvatartottSzemelyesAdatai>(s => s.FogvSzemAdatok)
                         .WithRequired(s => s.Fogvatartott)
                         .HasForeignKey(s => s.FogvatartottId);

            modelBuilder.Entity<IntezetiObjektum>()
                        .HasMany<Korlet>(s => s.Korletek)
                        .WithRequired(s => s.IntezetiObjektum)
                        .HasForeignKey(s => s.IntezetiObjektumId);



        }

        /// <summary>
        /// Kikapcsolja a törölt szűrést
        /// </summary>
        /// <typeparam name="TDbSetName"></typeparam>
        /// <param name="selector"></param>
        public void DisableToroltFlFilter<TDbSetName>(Expression<Func<KonasoftBVFonixContext, TDbSetName>> selector)
        {
            var dbSetName = ((MemberExpression)selector.Body).Member.Name;
            this.DisableFilter(dbSetName);
        }

        /// <summary>
        /// Bekapcsolja a törölt szűrést
        /// </summary>
        /// <typeparam name="TDbSetName"></typeparam>
        /// <param name="selector"></param>
        public void EnableToroltFlFilter<TDbSetName>(Expression<Func<KonasoftBVFonixContext, TDbSetName>> selector)
        {
            var dbSetName = ((MemberExpression)selector.Body).Member.Name;
            this.EnableFilter(dbSetName);
        }

        private void CreateFiltersOnAllEntities(DbModelBuilder modelBuilder)
        {
            //---------------------------------
            // Reflection-ös módszer, aminél lassabban működik az alkalmazás
            //---------------------------------

            // táblák keresése
            var tables = this.GetType().GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            //foreach (var item in tables.ToList())
            //{
            //    var property = item.PropertyType.GetGenericArguments()[0].GetProperty(nameof(SoftDeleteEntity.TOROLT_FL));

            //    if (property != null && item.PropertyType.GetGenericArguments()[0].IsSubclassOf(typeof(SoftDeleteEntity)))
            //        modelBuilder.Filter(item.PropertyType.GetGenericArguments()[0].Name, (SoftDeleteEntity b) => b.TOROLT_FL, false);
            //}
            //return;

            /// új törölt fl szűrés generáló 

            // meghívandó 4 paraméteres Filter extension methodok lekérése
            var query = (from type in typeof(EntityFramework.DynamicFilters.DynamicFilterExtensions).Assembly.GetTypes()
                         where type.IsSealed && !type.IsGenericType && !type.IsNested
                         from method in type.GetMethods(BindingFlags.Static
                             | BindingFlags.Public | BindingFlags.NonPublic)
                         where method.IsDefined(typeof(ExtensionAttribute), false)
                         where method.GetParameters()[0].ParameterType == modelBuilder.GetType()
                         where method.Name == "Filter"
                         where method.GetParameters().Count() == 4
                         select method).ToList();

            // a pontos Filter method túlterhelés megkeresése
            var filterMethod = query.Single(x => x.GetParameters()[3].ParameterType.Name == "Object" && x.GetParameters()[3].Name == "globalValue");

            var toroltFl = nameof(SoftDeleteEntity.TOROLT_FL);
            foreach (var item in tables.ToList())
            {
                // dbset típusa
                Type dbSetType = item.PropertyType.GetGenericArguments()[0];
                string dbSetName = item.Name;

                var property = dbSetType.GetProperty(toroltFl);

                if (property != null && dbSetType.IsSubclassOf(typeof(SoftDeleteEntity)))
                {
                    // linq paraméter létrehozása, lambda bal oldala: x=>
                    ParameterExpression p = Expression.Parameter(dbSetType, "x");

                    // linq property létrehozása, lambda jobb oldala: => x.TOROLT_FL
                    MemberExpression prop = Expression.Property(p, toroltFl);

                    // linq kifejezés megadása
                    var delegateType = typeof(Func<,>).MakeGenericType(dbSetType, typeof(bool));

                    // linq teljes összeállítása
                    var lambda = Expression.Lambda(delegateType, prop, p);

                    // method paraméter listájának összeállítása
                    object[] o = new object[] { modelBuilder, dbSetName, lambda, false };

                    // generikus híváshoz a metódus előkészítése
                    var m = filterMethod.MakeGenericMethod(dbSetType, typeof(bool));
                    // Filter method hívása
                    m.Invoke(modelBuilder, o);
                }
            }
        }

        //public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        //{

        //    TEntity dbEntity = this.Set<TEntity>().SingleOrDefault(x => x.Id == entity.Id);
        //    if (dbEntity != null)
        //    {
        //        this.Entry(dbEntity).CurrentValues.SetValues(entity);
        //        this.SaveChanges();
        //    }
        //}

        //public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        //{
        //    this.Set<TEntity>().Add(entity);
        //    this.SaveChanges();
        //}

        //public void Delete<TEntity>(int id) where TEntity : BaseEntity
        //{
        //    var dbEntity = this.Set<TEntity>().SingleOrDefault(x => x.Id == id);
        //    if (dbEntity != null)
        //    {
        //        this.Set<TEntity>().Remove(dbEntity);
        //        this.SaveChanges();
        //    }
        //}

        //public TEntity FindById<TEntity>(int id) where TEntity : BaseEntity
        //{
        //    return this.Set<TEntity>().SingleOrDefault(x => x.Id == id);
        //}

        Tranzakcio CreateTranzakcio()
        {
            var tranzakcio = new Tranzakcio();
            tranzakcio.RogzitesDatum = DateTime.Now;
            tranzakcio.Id = this.Set<Tranzakcio>().Max(x => x.Id) + 1;
            ITranzakcioAdatKontextusFunctions appsettingsFunctions = InjectionKernel.Instance.GetInstance<ITranzakcioAdatKontextusFunctions>();

            tranzakcio.RogzitoIntezetId = appsettingsFunctions.Kontextus.RogzitoIntezetId;
            tranzakcio.RogzitoSzemelyId = appsettingsFunctions.Kontextus.SzemelyzetId;

            this.Set<Tranzakcio>().Add(tranzakcio);
            return tranzakcio;
        }

    }
}



