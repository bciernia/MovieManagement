using System;
using System.Linq;
using Xunit;
using MovieManagement.App.Abstract;
using MovieManagement.App.Helpers;
using MovieManagement.App.Managers;
using MovieManagement.Domain.Entity;
using MovieManagement.App.Concrete;
using Moq;
using FluentAssertions;
using MovieManagement.Domain.Helpers;

namespace MovieManagement.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanAddMovieToList()
        {
            //Arrange
            Movie movie = new Movie(1, "Test", MovieType.Action, 2020, "Test");
            var movieServiceMock = new Mock<IService<Movie>>();
            var informationProviderMock = new Mock<InformationProvider>();

            movieServiceMock.Setup(m => m.GetMovieById(1)).Returns(movie);
            informationProviderMock.Setup(m => m.GetNumericInputKey()).Returns(1);
            informationProviderMock.Setup(m => m.GetInputString()).Returns("Test");
            informationProviderMock.Setup(m => m.GetNumericValue()).Returns(2020);

            var testingObject = new MovieManager(new MenuActionService(), movieServiceMock.Object, informationProviderMock.Object);
            //Act

            var result = testingObject.AddNewMovie();
            //Assert
            result.Should().Be(movie.Id);
            movieServiceMock.Verify(m => m.GetMovieById(1), Times.Once);
            movieServiceMock.Verify(m => m.AddMovie(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public void CanRemoveMovieWithProperId()
        { 
            Movie movie = new Movie(1, "Test", MovieType.Action, 2020, "Test");
            var mock = new Mock<IService<Movie>>();
            mock.Setup(m => m.GetMovieById(1)).Returns(movie);
            mock.Setup(m => m.RemoveMovie(It.IsAny<Movie>()));
            var manager = new MovieManager(new MenuActionService(), mock.Object, new InformationProvider());

            manager.RemoveMovieById(movie.Id);

            mock.Verify(m => m.RemoveMovie(movie));
        }

    }
}
