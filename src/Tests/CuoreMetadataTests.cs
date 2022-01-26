using System;
using System.Linq;
using NUnit.Framework;
using Should;
using NationalInstruments.Tdms;
using System.Diagnostics;
namespace Tests
{

    [TestFixture]
    public class CuoreMetadataTests
    {
        private File _file;

        [SetUp]
        public void Setup()
        {
            _file = new File(Constants.CuoreMetadataTestFile).Open();
        }

        [TearDown]
        public void TearDown()
        {
            _file.Dispose();
        }

        [Test]
        public void Should_Contain_NI_Props()
        {
            var groups = _file.Groups.Select(x => x.Value);
			Debug.WriteLine("Groups:");
            foreach(var grp in groups)
			{
				Debug.WriteLine($" - {grp.Name}");
			}

            var group_demod = _file.Groups["Demodulated data"];
            var chs = group_demod.Channels;
			Debug.WriteLine("\nChannels:");
            foreach (var ch in chs)
            {
                Debug.WriteLine($" - {ch.Key}");
            }

            var properties = group_demod.Channels["A3"].Properties;
            Debug.WriteLine("\nProperties:");
            foreach (var prop in properties)
            {
                Debug.WriteLine($" - {prop.Key} : {prop.GetType().ToString()}");
            }

            properties.Count.ShouldEqual(15);
          
        }
    }
}
