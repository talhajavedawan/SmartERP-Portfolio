using SmartERP.Showcase;
using Xunit;

namespace SmartERP.Showcase.Tests;

public sealed class ApprovalWorkflowTests
{
    [Fact]
    public void Workflow_FollowsTheRequiredApprovalSequence()
    {
        var workflow = new ApprovalWorkflow();

        workflow.Submit();
        workflow.CompleteReview();
        workflow.Approve();

        Assert.Equal(ApprovalStatus.Approved, workflow.Status);
    }

    [Fact]
    public void Approve_ThrowsWhenReviewHasNotCompleted()
    {
        var workflow = new ApprovalWorkflow();
        workflow.Submit();

        Assert.Throws<InvalidOperationException>(() => workflow.Approve());
    }
}
