#!/bin/zsh

echo "\n🔍 Validating JSON files...\n"

find . -type f -name "*.json" \
  ! -path "*/obj/*" \
  ! -path "*/bin/*" \
  -print0 | while IFS= read -r -d '' file; do
    if jq empty "$file" 2>/dev/null; then
      echo "\033[1;32m✅ $file is valid JSON\033[0m"  # Green
    else
      echo "\033[1;31m❌ $file is invalid JSON\033[0m"  # Red
    fi
done

echo "\n✅ Validation complete.\n"