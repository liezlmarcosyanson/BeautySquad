using FluentValidation;

namespace IAT.Application
{
    public class InfluencerCreateValidator : AbstractValidator<InfluencerCreateRequest>
    {
        public InfluencerCreateValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required");
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Invalid email");
        }
    }

    public class CampaignCreateValidator : AbstractValidator<CampaignCreateRequest>
    {
        public CampaignCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Budget).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be >= StartDate");
        }
    }

    public class ContentSubmissionValidator : AbstractValidator<ContentSubmissionCreateRequest>
    {
        public ContentSubmissionValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.CampaignId).NotEmpty();
            RuleFor(x => x.InfluencerId).NotEmpty();
        }
    }
}
