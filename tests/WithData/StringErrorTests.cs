﻿namespace NetCoreResults.Tests.WithData;

public class StringErrorTests
{
    [Fact]
    public void When_initialize_with_data_then_result_is_success()
    {
        int data = 0;
        Result<int, string> result = data;

        Assert.True(result.IsSuccess());
        Assert.False(result.IsFailure());

        if (result.IsSuccess(out int v))
            Assert.Equal(data, v);
        else
            Assert.Fail("IsSuccess returns 'false' when expected 'true'");

        if (result.IsFailure(out string _))
            Assert.Fail("IsFailure returns 'true' when expected 'false'");

        Assert.Equal(data, result.Data);
        Assert.Throws<InvalidOperationException>(() => result.Error);

        var trimmedResult = result.TrimSuccess();
        Assert.True(trimmedResult.IsSuccess());
        Assert.False(trimmedResult.IsFailure());
        Assert.Throws<InvalidOperationException>(() => trimmedResult.Error);
    }

    [Fact]
    public void When_initialize_with_error_then_result_is_failure()
    {
        string error = "Error";
        Result<int, string> result = error;

        Assert.False(result.IsSuccess());
        Assert.True(result.IsFailure());

        if (result.IsSuccess(out int _))
            Assert.Fail("IsSuccess returns 'true' when expected 'false'");

        if (result.IsFailure(out string e))
            Assert.Equal(error, e);
        else
            Assert.Fail("IsFailure returns 'false' when expected 'true'");

        Assert.Throws<InvalidOperationException>(() => result.Data);
        Assert.Equal(error, result.Error);

        var trimmedResult = result.TrimSuccess();
        Assert.False(trimmedResult.IsSuccess());
        Assert.True(trimmedResult.IsFailure());
        Assert.Equal(error, trimmedResult.Error);
    }
}
