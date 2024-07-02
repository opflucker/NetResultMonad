﻿using NetResults;

namespace NetResult.Tests.WithData;

public class IntErrorTests
{
    [Fact]
    public void When_initialize_with_data_then_result_is_success()
    {
        string data = "Data";
        Result<string, int> result = data;

        Assert.True(result.IsSuccess());
        Assert.False(result.IsFailure());

        if (result.IsSuccess(out string v))
            Assert.Equal(data, v);
        else
            Assert.Fail("IsSuccess returns 'false' when expected 'true'");

        if (result.IsFailure(out int _))
            Assert.Fail("IsFailure returns 'true' when expected 'false'");

        Assert.Equal(data, result.Data);
        Assert.Throws<InvalidOperationException>(() => result.Error);

        var narrowedResult = result.Narrow();
        Assert.True(narrowedResult.IsSuccess());
        Assert.False(narrowedResult.IsFailure());
        Assert.Throws<InvalidOperationException>(() => narrowedResult.Error);
    }

    [Fact]
    public void When_initialize_with_error_then_result_is_failure()
    {
        int error = 0;
        Result<string, int> result = error;

        Assert.False(result.IsSuccess());
        Assert.True(result.IsFailure());

        if (result.IsSuccess(out string _))
            Assert.Fail("IsSuccess returns 'true' when expected 'false'");

        if (result.IsFailure(out int e))
            Assert.Equal(error, e);
        else
            Assert.Fail("IsFailure returns 'false' when expected 'true'");

        Assert.Throws<InvalidOperationException>(() => result.Data);
        Assert.Equal(error, result.Error);

        var narrowedResult = result.Narrow();
        Assert.False(narrowedResult.IsSuccess());
        Assert.True(narrowedResult.IsFailure());
        Assert.Equal(error, narrowedResult.Error);
    }
}