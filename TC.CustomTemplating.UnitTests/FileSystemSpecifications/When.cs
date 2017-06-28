using System;
using System.IO;
using MbUnit.Framework;
using System.Reflection;

namespace TC.CustomTemplating.UnitTests.FileSystemSpecifications
{
    /// <summary />
    [TestFixture]
    public class When
    {
        /// <summary />
        [Test]
        public void getting_EntryAssemblyLocation_given_there_is_no_EntryAssembly_null_should_be_returned()
        {
            var fileSystem = new FileSystem();
            var assembly = Assembly.GetEntryAssembly();  //Not available during unit testing
            Assert.AreEqual(fileSystem.EntryAssemblyLocation, assembly != null && !assembly.IsDynamic() ? assembly.Location : null);
        }

        /// <summary />
        [Test]
        public void getting_EntryAssemblyLocation_given_there_is_a_EntryAssembly_the_location_should_be_returned()
        {
            var domain = AppDomain.CreateDomain("test");
            
            try
            {
                domain.ExecuteAssembly(typeof (EntryAssemblyRunnner).Assembly.Location);
            }
            finally
            {
                AppDomain.Unload(domain);
            }
        }

        /// <summary />
        [Test]
        public void combining_a_path_the_combined_result_should_be_returned()
        {
            var fileSystem = new FileSystem();
            var path1 = @"c:\blsblab";
            var path2 = "test.tst";
            var expected = Path.Combine(path1, path2);
            Assert.AreEqual(fileSystem.Combine(path1, path2), expected);
        }

        /// <summary />
        [Test]
        public void getting_a_directory_of_a_path_the_directory_should_be_returned()
        {
            var fileSystem = new FileSystem();
            var path = @"c:\blsblab\test.tst";
            var expected = Path.GetDirectoryName(path);
            Assert.AreEqual(fileSystem.GetDirectoryName(path), expected);
        }

        /// <summary />
        [Test]
        public void checking_a_rooted_path_true_should_be_returned_when_path_is_rooted()
        {
            var fileSystem = new FileSystem();
            var path = @"c:\blsblab\test.tst";
            Assert.AreEqual(fileSystem.IsPathRooted(path), true);
        }

        /// <summary />
        [Test]
        public void checking_a_rooted_path_false_should_be_returned_when_path_is_not_rooted()
        {
            var fileSystem = new FileSystem();
            const string path = @"blsblab\test.tst";

            Assert.IsFalse(fileSystem.IsPathRooted(path));
        }

        /// <summary />
        [Test]
        public void checking_whether_a_file_exists_false_should_be_returned_when_file_does_not_exist()
        {
            var fileSystem = new FileSystem();
            
            var path = Path.GetTempFileName();
            File.Delete(path);
        
            Assert.IsFalse(File.Exists(path));
            Assert.IsFalse(fileSystem.FileExists(path));
        }

        /// <summary />
        [Test]
        public void checking_whether_a_file_exists_true_should_be_returned_when_file_does_exist()
        {
            var fileSystem = new FileSystem();

            var path = Path.GetTempFileName();
            File.WriteAllText(path, "this is a test");
            try
            {
                Assert.IsTrue(fileSystem.FileExists(path));
            }
            finally
            {
                File.Delete(path);
            }
        }

        /// <summary />
        [Test]
        public void reading_the_content_of_a_file_the_full_content_should_be_returned()
        {
            var fileSystem = new FileSystem();

            var path = Path.GetTempFileName();
            const string contents = "this is a test";
            File.WriteAllText(path, contents);
            try
            {
                Assert.AreEqual(fileSystem.ReadAllText(path), contents);
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}