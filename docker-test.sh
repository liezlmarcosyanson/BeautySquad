#!/bin/bash

# BeautySquad API Docker Testing Script
# This script tests the API running in Docker

set -e

API_URL="http://localhost:9000"
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

TESTS_PASSED=0
TESTS_FAILED=0

print_header() {
    echo -e "\n${BLUE}=== $1 ===${NC}\n"
}

print_success() {
    echo -e "${GREEN}✓${NC} $1"
    ((TESTS_PASSED++))
}

print_failure() {
    echo -e "${RED}✗${NC} $1"
    ((TESTS_FAILED++))
}

print_info() {
    echo -e "${YELLOW}ℹ${NC} $1"
}

print_test() {
    echo "  Testing: $1"
}

# Check if API is running
print_header "Checking API Health"
print_test "Swagger endpoint"

if curl -s "$API_URL/swagger/" | grep -q "swagger"; then
    print_success "Swagger UI is accessible"
else
    print_failure "Swagger UI is not accessible"
    print_info "API may not be fully started yet. Try again in a moment."
    exit 1
fi

# Test Campaigns endpoints
print_header "Testing Campaigns Endpoints"

# Create Campaign
print_test "POST /api/campaigns"
CAMPAIGN_RESPONSE=$(curl -s -X POST "$API_URL/api/campaigns" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Summer 2024 Campaign",
    "description": "Test campaign for Docker",
    "start": "2024-06-01T00:00:00Z",
    "end": "2024-08-31T23:59:59Z"
  }')

if echo "$CAMPAIGN_RESPONSE" | grep -q "Summer 2024 Campaign"; then
    CAMPAIGN_ID=$(echo "$CAMPAIGN_RESPONSE" | grep -o '"id":"[^"]*"' | head -1 | cut -d'"' -f4)
    print_success "Campaign created (ID: ${CAMPAIGN_ID:0:8}...)"
else
    print_failure "Failed to create campaign"
    print_info "Response: $CAMPAIGN_RESPONSE"
fi

# Get Campaign
if [ ! -z "$CAMPAIGN_ID" ]; then
    print_test "GET /api/campaigns/{id}"
    if curl -s "$API_URL/api/campaigns/$CAMPAIGN_ID" | grep -q "Summer 2024 Campaign"; then
        print_success "Campaign retrieved successfully"
    else
        print_failure "Failed to retrieve campaign"
    fi
fi

# List Campaigns
print_test "GET /api/campaigns"
if curl -s "$API_URL/api/campaigns" | grep -q "Summer 2024 Campaign"; then
    print_success "Campaigns list retrieved successfully"
else
    print_failure "Failed to retrieve campaigns list"
fi

# Test Influencers endpoints
print_header "Testing Influencers Endpoints"

# Create Influencer
print_test "POST /api/influencers"
INFLUENCER_RESPONSE=$(curl -s -X POST "$API_URL/api/influencers" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Test Influencer",
    "email": "test@example.com",
    "bio": "Test bio",
    "phone": "+1-555-0001",
    "geography": "USA",
    "tags": ["test", "docker"],
    "advocacyStatus": "Active"
  }')

if echo "$INFLUENCER_RESPONSE" | grep -q "Test Influencer"; then
    INFLUENCER_ID=$(echo "$INFLUENCER_RESPONSE" | grep -o '"id":"[^"]*"' | head -1 | cut -d'"' -f4)
    print_success "Influencer created (ID: ${INFLUENCER_ID:0:8}...)"
else
    print_failure "Failed to create influencer"
    print_info "Response: $INFLUENCER_RESPONSE"
fi

# Get Influencer
if [ ! -z "$INFLUENCER_ID" ]; then
    print_test "GET /api/influencers/{id}"
    if curl -s "$API_URL/api/influencers/$INFLUENCER_ID" | grep -q "Test Influencer"; then
        print_success "Influencer retrieved successfully"
    else
        print_failure "Failed to retrieve influencer"
    fi
fi

# Test Content Submissions
print_header "Testing Content Submissions Endpoints"

if [ ! -z "$CAMPAIGN_ID" ] && [ ! -z "$INFLUENCER_ID" ]; then
    # Create Submission
    print_test "POST /api/content-submissions"
    SUBMISSION_RESPONSE=$(curl -s -X POST "$API_URL/api/content-submissions" \
      -H "Content-Type: application/json" \
      -d "{
        \"campaignId\": \"$CAMPAIGN_ID\",
        \"influencerId\": \"$INFLUENCER_ID\",
        \"title\": \"Test Submission\",
        \"caption\": \"Test caption for Docker testing\"
      }")

    if echo "$SUBMISSION_RESPONSE" | grep -q "Test Submission"; then
        SUBMISSION_ID=$(echo "$SUBMISSION_RESPONSE" | grep -o '"id":"[^"]*"' | head -1 | cut -d'"' -f4)
        print_success "Content submission created (ID: ${SUBMISSION_ID:0:8}...)"
    else
        print_failure "Failed to create content submission"
        print_info "Response: $SUBMISSION_RESPONSE"
    fi

    # Get Submission
    if [ ! -z "$SUBMISSION_ID" ]; then
        print_test "GET /api/content-submissions/{id}"
        if curl -s "$API_URL/api/content-submissions/$SUBMISSION_ID" | grep -q "Test Submission"; then
            print_success "Submission retrieved successfully"
        else
            print_failure "Failed to retrieve submission"
        fi
    fi
fi

# Test error handling
print_header "Testing Error Handling"

print_test "404 Not Found (invalid campaign ID)"
RESPONSE=$(curl -s -w "\n%{http_code}" "$API_URL/api/campaigns/00000000-0000-0000-0000-000000000000")
HTTP_CODE=$(echo "$RESPONSE" | tail -n1)
if [ "$HTTP_CODE" = "404" ]; then
    print_success "404 error handled correctly"
else
    print_failure "Expected 404, got $HTTP_CODE"
fi

print_test "400 Bad Request (invalid JSON)"
RESPONSE=$(curl -s -w "\n%{http_code}" -X POST "$API_URL/api/campaigns" \
  -H "Content-Type: application/json" \
  -d '{invalid json}')
HTTP_CODE=$(echo "$RESPONSE" | tail -n1)
if [ "$HTTP_CODE" = "400" ] || [ "$HTTP_CODE" = "415" ]; then
    print_success "400/415 error handled correctly"
else
    print_failure "Expected 400/415, got $HTTP_CODE"
fi

# Database tests
print_header "Testing Database Connectivity"

print_test "SQL Server connection"
if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = "dbo"' &>/dev/null; then
    print_success "SQL Server connection successful"
else
    print_failure "SQL Server connection failed"
fi

print_test "Database BeautySquadDb exists"
if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'USE BeautySquadDb; SELECT 1' &>/dev/null; then
    print_success "Database BeautySquadDb exists and is accessible"
else
    print_failure "Database BeautySquadDb not found or not accessible"
fi

# Summary
print_header "Test Summary"
TOTAL=$((TESTS_PASSED + TESTS_FAILED))
echo "Total Tests: $TOTAL"
echo -e "Passed: ${GREEN}$TESTS_PASSED${NC}"
echo -e "Failed: ${RED}$TESTS_FAILED${NC}"
echo ""

if [ $TESTS_FAILED -eq 0 ]; then
    echo -e "${GREEN}All tests passed!${NC}"
    exit 0
else
    echo -e "${RED}Some tests failed!${NC}"
    exit 1
fi
