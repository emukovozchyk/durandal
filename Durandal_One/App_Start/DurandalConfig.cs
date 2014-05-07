using System.Web.Optimization;
using Durandal_One.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof(Durandal_One.App_Start.DurandalConfig), "PreStart")]

namespace Durandal_One.App_Start
{
    public static class DurandalConfig
    {
        public static void PreStart()
        {
            BsonClassMap.RegisterClassMap<Person>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.Id));
                cm.GetMemberMap(c => c.Id).SetRepresentation(BsonType.ObjectId);
            });

            DurandalBundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}