using System;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.PathResolverSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_resolving
    {
        /// <summary />
        [Test]
        public void a_rooted_file_name_the_file_name_itself_is_returned()
        {
            var fileSystem = new Mock<IFileSystem>(); 
            var pathResolver = new PathResolver(fileSystem.Object);

            var path = "this is a rooted path";
            fileSystem.Setup(f => f.IsPathRooted(path)).Returns(true);

            var result = pathResolver.ResolvePath(path, null);
            Assert.AreEqual(path, result);
        }

        /// <summary />
        [Test]
        public void a_non_rooted_file_found_in_the_tranformer_path_the_file_name_is_combined_with_the_tranformer_path()
        {
            var fileSystem = new Mock<IFileSystem>();
            var pathResolver = new PathResolver(fileSystem.Object);

            var path = "this is a non rooted path";
            var transformerPath = "this is a transformer path";
            var combinedPath = "this is a combined path";

            fileSystem.Setup(f => f.IsPathRooted(path)).Returns(false);
            fileSystem.Setup(f => f.Combine(transformerPath, path)).Returns(combinedPath);
            fileSystem.Setup(f => f.FileExists(combinedPath)).Returns(true);

            var result = pathResolver.ResolvePath(path, transformerPath);
            Assert.AreEqual(result, combinedPath);
        }

        /// <summary />
        [Test]
        public void a_non_rooted_file_is_not_found_in_the_tranformer_path_nor_an_entry_assembly_path_is_available_the_path_is_returend()
        {
            var fileSystem = new Mock<IFileSystem>();
            var pathResolver = new PathResolver(fileSystem.Object);

            var path = "this is a non rooted path";
            var transformerPath = "this is a transformer path";
            var combinedPath = "this is a combined path";

            fileSystem.Setup(f => f.IsPathRooted(path)).Returns(false);
            fileSystem.Setup(f => f.Combine(transformerPath, path)).Returns(combinedPath);
            fileSystem.Setup(f => f.FileExists(combinedPath)).Returns(false);
            fileSystem.Setup(f => f.EntryAssemblyLocation).Returns((string) null);

            var result = pathResolver.ResolvePath(path, transformerPath);
            Assert.AreEqual(result, path);
        }

        /// <summary />
        [Test]
        public void a_non_rooted_file_found_in_the_entry_assemby_path_the_file_name_is_combined_with_the_tranformer_path()
        {
            var fileSystem = new Mock<IFileSystem>();
            var pathResolver = new PathResolver(fileSystem.Object);

            var path = "this is a non rooted path";
            var assemblyLocation = "this is the assembly location";
            var assemblyFolder = "this is the assembly folder";
            var combinedPath = "this is a combined path";

            fileSystem.Setup(f => f.IsPathRooted(path)).Returns(false);
            fileSystem.Setup(f => f.EntryAssemblyLocation).Returns(assemblyLocation);
            fileSystem.Setup(f => f.GetDirectoryName(assemblyLocation)).Returns(assemblyFolder);
            fileSystem.Setup(f => f.Combine(assemblyFolder, path)).Returns(combinedPath);
            fileSystem.Setup(f => f.FileExists(combinedPath)).Returns(true);

            var result = pathResolver.ResolvePath(path, null);
            Assert.AreEqual(result, combinedPath);
        }

        /// <summary />
        [Test]
        public void a_non_rooted_file_not_found_in_the_entry_assemby_folder_the_file_name_is_returned()
        {
            var fileSystem = new Mock<IFileSystem>();
            var pathResolver = new PathResolver(fileSystem.Object);

            var path = "this is a non rooted path";
            var assemblyLocation = "this is the assembly location";
            var assemblyFolder = "this is the assembly folder";
            var combinedPath = "this is a combined path";

            fileSystem.Setup(f => f.IsPathRooted(path)).Returns(false);
            fileSystem.Setup(f => f.EntryAssemblyLocation).Returns(assemblyLocation);
            fileSystem.Setup(f => f.GetDirectoryName(assemblyLocation)).Returns(assemblyFolder);
            fileSystem.Setup(f => f.Combine(assemblyFolder, path)).Returns(combinedPath);
            fileSystem.Setup(f => f.FileExists(combinedPath)).Returns(false);

            var result = pathResolver.ResolvePath(path, null);
            Assert.AreEqual(result, path);
        }
    }
}