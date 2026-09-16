# Selected engineering challenges

## Entity Framework state conflicts

### Situation

Long-lived WPF screens sometimes passed entities between repositories or contexts. EF could then report that an object belonged to another context or that another entity with the same key was already tracked.

### Approach

- Treat identifiers and DTO-like data as boundaries between workflows.
- Keep context lifetimes explicit and avoid sharing tracked graphs across screens.
- Query only the relationships required for the use case.
- Use no-tracking reads where updates are not required.
- Attach/update entities deliberately rather than relying on incidental graph state.

### Lesson

Desktop applications can accidentally make a data context behave like global state. Clear ownership and short, purposeful units of work make failures easier to reason about.

## Memory pressure in long-running sessions

### Situation

Large grids, previews, event handlers, cached objects, and Office COM objects accumulated during long user sessions.

### Approach

- Measure which screens and workflows increased retained memory.
- Dispose streams and large document resources deterministically.
- Unsubscribe event handlers when views are closed.
- Release Office COM references in the correct order.
- Avoid loading full graphs or thousands of records when a summary is sufficient.

### Lesson

Garbage collection cannot correct application-level ownership mistakes. Resource lifetimes must be designed, especially around native interop.

## Idle-session security across 100+ windows

### Situation

The application needed to log users out after 15 minutes of inactivity without duplicating timers and handlers in every window.

### Approach

Centralise activity tracking, observe relevant input events at the application boundary, reset one session timer, and route expiry through a single logout workflow that safely closes or redirects open views.

### Lesson

Cross-cutting behaviour belongs at an application boundary. A central design is more consistent and testable than per-window patches.

## Self-referencing permission trees

### Situation

Permission data used parent/child relationships and was displayed in hierarchical controls. Incorrect parent references or seeding order could create missing nodes, cycles, or constraint failures.

### Approach

- Separate stable keys from display ordering.
- Insert parents before children.
- Validate that parent references exist and that the graph is acyclic.
- Build the UI hierarchy from validated data rather than trusting every database row.

### Lesson

Hierarchical configuration is still domain data. It benefits from invariants, validation, and migration tests.

## Outlook integration at operational scale

### Situation

The desktop client surfaced inboxes, sent mail, public folders, HTML previews, attachments, subfolders, and unread state across thousands of items.

### Approach

Constrain loading, avoid retaining COM-backed objects beyond the immediate operation, copy required values into managed models, and isolate integration failures from the rest of the ERP session.

### Lesson

External integrations should be treated as unreliable boundaries, with narrow adapters and deliberate cleanup.
