using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookDesk.DataInterface;
using BookDesk.Domain;
using BookDesk.Processor;
using Moq;
using Xunit;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTest
    {
        private readonly DeskBookingRequest _request;
        private readonly List<Desk> _availableDesks;
        private readonly DeskBookingRequestProcessor _processor;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private readonly Mock<IDeskRepository> _deskRepositoryMock;

        public DeskBookingRequestProcessorTest()
        {
            _request = new DeskBookingRequest
            {
                FirstName = "David",
                LastName = "Smith",
                Email = "david.smith@gmail.com",
                Date = new DateTime(2020, 03, 13)
            };
            _availableDesks = new List<Desk> { new Desk { Id = 1 } };
           // _availableDesks = new List<Desk> { new Desk() };
            _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
            _deskRepositoryMock = new Mock<IDeskRepository>();
            _deskRepositoryMock.Setup(x => x.GetAvailableDesks(_request.Date)).Returns(_availableDesks);            
            _processor = new DeskBookingRequestProcessor(_deskBookingRepositoryMock.Object,_deskRepositoryMock.Object);
        }
        [Fact]
        public void ShoulReturnDeskBookingResultWithRequestValues()
        {
            
            //var processor = new DeskBookingRequestProcessor();
           DeskBookingResult result= _processor.BookDesk(_request);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
            Assert.Equal(_request.LastName, result.LastName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

        }
        [Fact]
        public void ShouldThrowExceptionIfRequestISNull()
        {
           // var processor = new DeskBookingRequestProcessor();
            var exception=Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));
            Assert.Equal("request", exception.ParamName);
        }
        [Fact]
        public void ShouldSaveDeskBooking()
        {
            DeskBooking saveDeskBooking = null;
            _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>())).Callback<DeskBooking>(deskBooking =>
            {
                saveDeskBooking = deskBooking;
            });
            _processor.BookDesk(_request);
            _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);
            Assert.NotNull(saveDeskBooking);
            Assert.Equal(_request.FirstName, saveDeskBooking.FirstName);
            Assert.Equal(_request.LastName, saveDeskBooking.LastName);
            Assert.Equal(_request.Email, saveDeskBooking.Email);
            Assert.Equal(_request.Date, saveDeskBooking.Date);
            Assert.Equal(_availableDesks.First().Id, saveDeskBooking.DeskId);

        }
        [Fact]
        public void ShouldNotSaveDeskBookingIfNoDeskAvailable()
        {
            _availableDesks.Clear();
            //To ensure that no desk is available
            _processor.BookDesk(_request);
            _deskBookingRepositoryMock.Verify(x=>x.Save(It.IsAny<DeskBooking>()),Times.Never);
                   
        }
        [Theory]
        [InlineData(DeskBookingResultCode.Success,true)]
        [InlineData(DeskBookingResultCode.NoDeskAvailable,false)]
        public void ShouldReturnExpectedResultCode(DeskBookingResultCode expectedResultCode,bool isDeskAvailable)
        {
            if(!isDeskAvailable)
            {
                _availableDesks.Clear();
            }
            var result = _processor.BookDesk(_request);
            Assert.Equal(expectedResultCode, result.Code);
        }
        [Theory]
        [InlineData(5, true)]
        [InlineData(null, false)]
        public void ShouldReturnExpectedDeskBookingId(int? expectedDeskBookingId, bool isDeskAvailable)
        {
            if (!isDeskAvailable)
            {
                _availableDesks.Clear();
            }
            else
            {
                _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>())).Callback<DeskBooking>(deskBooking =>
                {
                    deskBooking.Id = expectedDeskBookingId.Value;
                });
            }
            var result = _processor.BookDesk(_request);
            Assert.Equal(expectedDeskBookingId, result.DeskBookingId);
        }

    }
}
