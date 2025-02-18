#!/bin/bash

# Path to the version.json file in the root directory
VERSION=$(jq -r '.version' ./version.json)

# Output the version so it can be used in the GitHub Actions environment
echo "$VERSION"