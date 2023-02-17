﻿using System.Net.Http.Json;
using BlogApplication.Domain.Entities;
using FluentAssertions;
using test.Configuration.Base;
using test.Configuration.Containers;
using test.Utils;

namespace test.BlogApplication.Api.Controllers.PostController;

[Collection(nameof(IntegrationContainerCollection))]
public class CreatePost : TestConfigurationBase
{
    private readonly Post _postCreated;
    public CreatePost(PostgresTestContainer postgresTestContainer) : base(postgresTestContainer)
    {
        _postCreated = PostJson.PostCreated();
    }

    /// <summary>
    /// CreateNewPostValidateValuesWithPostExpected
    /// </summary>
    [Fact]
    public async void CreatePostShouldReturnAttributesAreAsserted()
    {
        var uri = new Uri($"{HttpClient.BaseAddress}Post/api/posts");
        var response = await HttpClient.PostAsync(uri, JsonContent.Create(_postCreated));
        var result = response.Content.ReadFromJsonAsync<Post>().Result;
        result!.Id.Should().Be(_postCreated.Id.ToString());
        result!.Author.Should().Be(_postCreated.Author);
    }

}