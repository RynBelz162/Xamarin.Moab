using Xamarin.Nuke;

namespace Xamarin.Moab.Forms.Test.iOS.MoabFiles
{
	public class CacheConfiguration
	{
		public DataCachingStrategy DataCachingStrategy { get; }
		public string CacheName { get; }
		public System.nint CacheSizeMb { get; }

		public CacheConfiguration(DataCachingStrategy dataCachingStrategy, string cacheName, System.nint cacheSizeMb)
        {
			DataCachingStrategy = dataCachingStrategy;
			CacheName = cacheName;
			CacheSizeMb = cacheSizeMb;
        }
	}
}

