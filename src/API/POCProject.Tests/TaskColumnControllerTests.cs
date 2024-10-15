using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POCProject.API.Controllers;
using POCProject.Models.Request;
using POCProject.Models.Response;
using POCProject.Services.Contract;
using POCProject.Services.DtoMapperProfile;

namespace POCProject.Tests;

/// <summary>
/// Unit tests for the <see cref="TaskColumnController"/> class, verifying the behavior of its actions.
/// This includes tests for creating a new task column, retrieving existing columns, and handling cases 
/// where no columns are available.
/// </summary>
[TestFixture]
public class TaskColumnControllerTests
{
    private TaskColumnController _controller;
    private Mock<ITaskColumnService> _mockService;
    private IMapper _mapper;

    /// <summary>
    /// Sets up the test environment before each test.
    /// Initializes mocks and the controller instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<ITaskColumnService>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        _mapper = new Mapper(config);
        _controller = new TaskColumnController(_mockService.Object);
    }

    /// <summary>
    /// Tests the <see cref="TaskColumnController.CreateColumnAsync"/> method.
    /// Verifies that a valid task column model results in a successful response.
    /// </summary>
    [Test]
    public async Task CreateColumnAsync_ValidModel_ReturnsOk()
    {
        // Arrange
        var taskColumn = new TaskColumnModel { Name = "New Column", SortOrder = 1 };
        var response = new BusinessResponse(201, true, taskColumn, "Column created successfully.");
        _mockService.Setup(s => s.AddColumnAsync(taskColumn, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(response);

        // Act
        var result = await _controller.CreateColumnAsync(taskColumn, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(response, okResult.Value);
    }

    /// <summary>
    /// Tests that the <see cref="TasksController.CreateColumnAsync"/> method
    /// calls the service and returns a <see cref="BadRequestObjectResult"/> 
    /// when given a valid model that results in a failure to create a column.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Test]
    public async Task CreateColumnAsync_ValidModel_ReturnsBadRequestWhenCreationFails()
    {
        // Arrange
        var taskColumn = new TaskColumnModel { Name = "New Column", SortOrder = 1 };
        var response = new BusinessResponse(400, true, taskColumn, "Column Not Created.");
        _mockService.Setup(s => s.AddColumnAsync(taskColumn, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(response);

        // Act
        var result = await _controller.CreateColumnAsync(taskColumn, CancellationToken.None);

        // Assert
        _mockService.Verify(s => s.AddColumnAsync(taskColumn, It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var okResult = result as BadRequestObjectResult;
        Assert.AreEqual(400, okResult.StatusCode);
        Assert.AreEqual(response, okResult.Value);
    }

    /// <summary>
    /// Tests the <see cref="TaskColumnController.GetColumnsAsync"/> method.
    /// Verifies that existing columns return a successful response with the correct data.
    /// </summary>
    [Test]
    public async Task GetColumnsAsync_ColumnsExist_ReturnsOkWithColumns()
    {
        // Arrange
        var columns = new List<TaskColumnModel>
        {
            new TaskColumnModel { Name = "Column 1", SortOrder = 1 },
            new TaskColumnModel { Name = "Column 2", SortOrder = 2 }
        };

        var response = new BusinessResponse
        {
            StatusCode = 200,
            Response = true,
            Data = columns,
            Message = "Columns retrieved successfully."
        };

        _mockService.Setup(s => s.GetColumnsAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(response);

        // Act
        var result = await _controller.GetColumnsAsync(CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(200, okResult.StatusCode);
        var businessResponse = okResult.Value as BusinessResponse;
        Assert.IsNotNull(businessResponse);
        Assert.AreEqual(columns, businessResponse.Data);
    }

    /// <summary>
    /// Tests the <see cref="TaskColumnController.GetColumnsAsync"/> method.
    /// Verifies that when no columns are available, a NotFound result is returned.
    /// </summary>
    [Test]
    public async Task GetColumnsAsync_NoColumns_ReturnsNotFound()
    {
        // Arrange
        var response = new BusinessResponse { StatusCode = 404, Response = false, Data = null, Message = "No columns found." };
        _mockService.Setup(s => s.GetColumnsAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(response);

        // Act
        var result = await _controller.GetColumnsAsync(CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);

        // Optionally cast to NotFoundObjectResult
        var notFoundResult = result as NotFoundObjectResult;

        // Assert that the status code is 404
        Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);

        // Assert that the value is as expected
        var returnedResponse = notFoundResult.Value as BusinessResponse;
        Assert.IsNotNull(returnedResponse);
        Assert.AreEqual(response.StatusCode, returnedResponse.StatusCode);
        Assert.AreEqual(response.Message, returnedResponse.Message);
    }
}