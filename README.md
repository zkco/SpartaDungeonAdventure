# SpartaDungeonAdventure
SpartaBootCamp Week 4 Project

프로젝트 기간 10.29 - 10.30

예비군으로 인해 강의 수강 일정 딜레이되어 하루만에 만들어야하는 상황

**구현 범위** (예정)

1. 기본 이동 및 점프
- Input Action 패키지를 이용하여 구현, WASD를 이용한 이동과 Space를 사용한 점프 구현 및 마우스 델타 값을 InputAction으로부터 입력받아 FPS식 화면 회전 구현

2. 체력 UI
- 2D Sprite 패키지를 이용하여 Filled 타입으로 HP 아이콘에 구현

3. Raycast를 이용한 환경조사
- Camera 중앙을 기준으로 1f 길이의 탐색 Ray를 사용하여 해당 범위에 상호작용 가능한 레이어의 물건이 있을 경우 해당 정보를 표시

4. 점프패들
- 모델을 구할 시간이 없어 Cube를 위아래로 얇게 펴서 밟을 시(Collider Trigger로 구현) AddForce Forcemode.Impulse로 점프 구현

5. ScriptableObject를 통한 아이템 데이터관리
- 아이템 데이터를 ScriptableObject, CreateAssetMenu를 통해 생성 및 관리를 쉽게 하고 enum을 통해 아이템 타입을 정함

6. 아이템 사용
- 아이템 타입에 따른 사용법 구분
  Dura 타입은 Coroutine을 통해 일정 시간 효과 발생
  Temp 타입은 순간적으로 효과 발생
