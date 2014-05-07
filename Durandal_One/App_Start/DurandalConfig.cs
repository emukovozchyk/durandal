using System.Web.Optimization;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof(Durandal_One.App_Start.DurandalConfig), "PreStart")]

namespace Durandal_One.App_Start
{
    public static class DurandalConfig
    {
        public static void PreStart()
        {
            // Add your start logic here
            DurandalBundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}