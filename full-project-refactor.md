# Plan: Full Project Refactor (Mulletaflix)

> **Objective**: Perform a comprehensive refactoring of the Mulletaflix project to align with Antigravity Kit standards, focusing on Security, Clean Code, Performance, and Modern Architecture.

---

## 📋 Overview
- **Project Type**: BACKEND (C# / .NET)
- **Goal**: Full-spectrum modernization and technical debt elimination.
- **Success Criteria**: 
  - 100% pass on `checklist.py` and `security_scan.py`.
  - Zero hardcoded secrets in codebase.
  - Mitigation of all command injection and path traversal risks.
  - Implementation of "Strangler Fig" pattern for complex legacy modules.

---

## 🛠️ Tech Stack
- **Language**: C# 10/11+
- **Framework**: .NET 6.0/7.0+
- **Security**: ProcessStartInfo.ArgumentList, Path validation, Centralized Config.
- **Standards**: Antigravity Clean Code, SOLID, DRY.

---

## 🏗️ Refactoring Phases

### 🛡️ Phase 1: Foundation & Security Hardening
*Focus: Security, Secrets, and Global Infrastructure.*

- [ ] **Task 1.1: Global Secret Audit**
  - **Agent**: `security-auditor`
  - **Skill**: `vulnerability-scanner`
  - **Input**: All project files.
  - **Output**: List of hardcoded strings in `CODEBASE.md`.
  - **Verify**: `grep -r "REPLACEME_IN_CONFIG" .` should show all moved secrets.
- [ ] **Task 1.2: Advanced Process Injection Remediation**
  - **Agent**: `code-archaeologist`
  - **Skill**: `clean-code`
  - **Input**: Remaining modules in `MediaBrowser.MediaEncoding`.
  - **Output**: Refactored `Process.Start` calls using `ArgumentList`.
  - **Verify**: Static analysis check for `Arguments = ` assignments.
- [ ] **Task 1.3: Centralized Configuration Implementation**
  - **Agent**: `backend-specialist`
  - **Skill**: `nodejs-best-practices` (adapted to C# Options Pattern)
  - **Input**: `appsettings.json` and Options classes.
  - **Output**: Typed configuration for all external providers.
  - **Verify**: Unit tests for configuration binding.

### 🔌 Phase 2: API & Controller Modernization
*Focus: API performance and validation.*

- [ ] **Task 2.1: Controller Logic Extraction**
  - **Agent**: `backend-specialist`
  - **Skill**: `api-patterns`
  - **Input**: Controllers in `Jellyfin.Api/Controllers`.
  - **Output**: Logic moved from Controllers to Service layer.
    - [ ] `ItemsController.cs` -> `IItemService`
    - [ ] `LibraryController.cs` -> `ILibraryService`
  - **Verify**: Slimmed down controller files (<200 LOC per controller).
- [ ] **Task 2.2: Input Validation Overhaul**
  - **Agent**: `security-auditor`
  - **Skill**: `vulnerability-scanner`
  - **Input**: API DTOs.
  - **Output**: Implementation of Request DTOs and validation for complex query parameters.
  - **Verify**: API integration tests for invalid input handling.

### 🎥 Phase 3: Media Encoding & Processing
*Focus: Simplification of complex logic.*

- [ ] **Task 3.1: MediaEncoder Modularization**
  - **Agent**: `code-archaeologist`
  - **Skill**: `refactoring-patterns`
  - **Input**: `MediaEncoder.cs`.
  - **Output**: Large methods extracted into specialized classes.
    - [x] FFmpeg capability detection -> `IFFmpegCapabilityManager`
    - [ ] Image extraction logic -> `IImageExtractionService`
    - [ ] Media probe logic -> `IMediaProbeService`
  - **Verify**: `MediaEncoder.cs` reduced size and improved readability.
- [ ] **Task 3.2: Subtitle Engine Refactor**
  - **Agent**: `backend-specialist`
  - **Skill**: `clean-code`
  - **Input**: `SubtitleEncoder.cs`.
  - **Output**: Unified interface for different subtitle formats.
  - **Verify**: Passing unit tests for subtitle conversion.

### 📺 Phase 4: Live TV & Streaming
*Focus: Modernizing the streaming stack.*

- [ ] **Task 4.1: Live TV Manager Cleanup**
  - **Agent**: `backend-specialist`
  - **Skill**: `clean-code`
  - **Input**: `RecordingsManager.cs` and `LiveTvManager.cs`.
  - **Output**: Decoupled recording logic from core TV management.
  - **Verify**: Successful recording test cycle.

### 📊 Phase 5: Final Verification & Audit
*Focus: ag-kit Compliance.*

- [ ] **Task 5.1: Full Project Checklist**
  - **Agent**: `orchestrator`
  - **Skill**: `checklist`
  - **Input**: Project root.
  - **Output**: Successful run of `.agent/scripts/checklist.py .`.
  - **Verify**: Zero "Critical" issues remaining.
- [ ] **Task 5.2: Documentation Sync (Obsidian)**
  - **Agent**: `documentation-writer`
  - **Skill**: `documentation-templates`
  - **Input**: Refactored codebase.
  - **Output**: Updated `PROJECT.md` and `ARCHITECTURE.md` in Obsidian.
  - **Verify**: Markdown files reflect new architecture.

---

## ✅ PHASE X: FINAL VERIFICATION
- [ ] **Security**: `python .agent/scripts/checklist.py .` ✅
- [ ] **Clean Code**: Standardized naming and logic ✅
- [ ] **Build**: `dotnet build` ✅
- [ ] **Date**: 2026-05-07
