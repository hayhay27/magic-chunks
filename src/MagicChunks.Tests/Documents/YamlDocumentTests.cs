﻿using System;
using MagicChunks.Documents;
using Xunit;

namespace MagicChunks.Tests.Documents
{
    public class YamlDocumentTests
    {
        [Fact]
        public void Transform()
        {
            // Arrange

            var document = new YamlDocument(@"a:
    x: 1
b: 2
c: 3");


            // Act

            document.ReplaceKey(new[] { "A", "y" }, "2");
            document.ReplaceKey(new[] { "a", "z", "t", "w" }, "3");
            document.ReplaceKey(new[] { "b" }, "5");
            document.ReplaceKey(new[] { "c", "a" }, "1");
            document.ReplaceKey(new[] { "c", "b" }, "2");
            document.ReplaceKey(new[] { "c", "b", "t" }, "3");
            document.ReplaceKey(new[] { "D" }, "4");

            var result = document.ToString();


            // Assert

            Assert.Equal(@"a:
  x: 1
  y: 2
  z:
    t:
      w: 3
b: 5
c:
  a: 1
  b:
    t: 3
d: 4
", result.ToLowerInvariant());
        }

        [Fact]
        public void ValidateEmptyPath()
        {
            // Assert
            YamlDocument document = new YamlDocument("");

            // Act
            ArgumentException result = Assert.Throws<ArgumentException>(() => document.ReplaceKey(new[] { "a", "", "b" }, ""));

            // Arrange
            Assert.True(result.Message?.StartsWith("There is empty items in the path."));
        }

        [Fact]
        public void ValidateWithespacePath()
        {
            // Assert
            YamlDocument document = new YamlDocument("");

            // Act
            ArgumentException result = Assert.Throws<ArgumentException>(() => document.ReplaceKey(new[] { "a", "   ", "b" }, ""));

            // Arrange
            Assert.True(result.Message?.StartsWith("There is empty items in the path."));
        }
    }
}