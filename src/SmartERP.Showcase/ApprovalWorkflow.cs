namespace SmartERP.Showcase;

public enum ApprovalStatus
{
    Draft,
    AwaitingReview,
    AwaitingApproval,
    Approved,
    Rejected
}

public sealed class ApprovalWorkflow
{
    public ApprovalStatus Status { get; private set; } = ApprovalStatus.Draft;

    public void Submit()
    {
        EnsureStatus(ApprovalStatus.Draft);
        Status = ApprovalStatus.AwaitingReview;
    }

    public void CompleteReview()
    {
        EnsureStatus(ApprovalStatus.AwaitingReview);
        Status = ApprovalStatus.AwaitingApproval;
    }

    public void Approve()
    {
        EnsureStatus(ApprovalStatus.AwaitingApproval);
        Status = ApprovalStatus.Approved;
    }

    public void Reject()
    {
        if (Status is not (ApprovalStatus.AwaitingReview or ApprovalStatus.AwaitingApproval))
        {
            throw new InvalidOperationException($"Cannot reject a workflow in {Status} status.");
        }

        Status = ApprovalStatus.Rejected;
    }

    private void EnsureStatus(ApprovalStatus expected)
    {
        if (Status != expected)
        {
            throw new InvalidOperationException(
                $"Expected {expected} status but the workflow is {Status}.");
        }
    }
}
