using PostgreSqlDotnetCore.Models;
using System;
namespace PostgreSqlDotnetCore.repositories
{
    public class TeamMemberRepository
    {
        private readonly List<TeamMember> _teamMembers;

        public TeamMemberRepository()
        {
            // Initialize the team member data
            _teamMembers = new List<TeamMember>
            {
                new TeamMember
                {
                    Name = "John Doe",
                    Position = "Chief Executive Officer",
                    ImageUrl = "ceo-image.png",
                    Bio = "John has over 15 years of experience in the financial industry and is passionate about helping individuals take control of their finances."
                },
                new TeamMember
                {
                    Name = "Jane Smith",
                    Position = "Chief Financial Officer",
                    ImageUrl = "cfo-image.png",
                    Bio = "Jane is a certified public accountant with a deep understanding of personal finance. She leads the team in developing innovative solutions to help our users achieve their financial goals."
                }
            };
        }

        public List<TeamMember> GetTeamMembers()
        {
            // Retrieve the list of team members
            return _teamMembers;
        }
    }

}