# D-20
- 최강의 기사가 되기위해 원시시대 던전에 들어간 플레이어. 20일 동안 살아남아서 세계관 최강 갓-기사가 되자!  
> 내일배움 캠프 Unity 6기 유니티 숙련주차 프로젝트  
> 2024.10.31 ~ 2024.11.07
> [영상](https://www.youtube.com/watch?v=cDnZxsi9H_A)

## 맴버 및 역할 분담
- 박수연 (팀장)
  - 자원 및 아이템
  - 리스폰 시스템
- 조윤진
  - 몬스터 AI 구현
- 오태호
  - 플레이어 구현
  - 시간 시스템
- 최어진
  - 건축 시스템
 
## 주요 구현 상세
### 데이터 관리
- https://github.com/mikito/unity-excel-importer?tab=readme-ov-file 을 활용
- 엑셀 파일로 몬스터, 아이템, 자원, 건축물 데이터를 관리
- 데이터 클래스를 만들고, 엑셀시트를 로드하는 So를 생성하여 이를 통해 싱글톤으로 원하는 데이터를 불러올 수 있는 DB를 구성
### Pooling
- GameObject 타입의 객체를 Pooling 하는 PoolingSystem을 구현
- Poolingsystem에서는, 오브젝트를 생성할때 Poolable컴포넌트 스크립트를 추가해주고, 해당 PoolingSystem을 저장해두어, 나중에 Disable될때 Pool에 반납하도록 하여 자동 반납시스템을 구현하였음.
- 아이템, 몬스터와 같이 id로 동적으로 생성되는 오브젝트에 대해서는, ILoabable 인터페이스를 구현하여,Init에서 해당 오브젝트의 Load 함수를 호출해 데이터를 로드할 수 있도록 하였음.
### 사용자 입력 관리
- new InputSystem을 사용하였으며, InvokeUnityEvent를 활용하였음
### 몬스터 AI 구현
- NaviMeshAgent를 활용해 몬스터의 이동을 구현함
- enum을 활용해 AI 상태를 판별하고, 상태에 알맞은 행동을 수행하도록 구현함.
- 지형을 Terrain을 사용하여 Nav 길찾기 알고리즘을 연산하는데 시간이 오래걸려 ai상태 업데이트에 코루틴을 활용하였었음.
### 건축 시스템 구현
- 충돌 감지와 Ray를 활용하여 건축물을 생성할 수 있는 위치를 판별하고, 생성 가능 여부에 따라 메터리얼을 변경하도록 하였음.
- 허공을 바라봐서 Ray에 건축물을 둘 수 있는 곳이 없는 경우, Ray의 끝에서 vector3.down으로 ray를 다시 쏘아 Ray의 끝 지점의 바닥에 오브젝트를 두도록 함.
### 인벤토리 구현
- 인벤토리 아이템 배열을 들고 배열을 관리하는 Inventory 클래스와, 사용자의 상호작용 및 UI 상호작용을 받는 InventoryControll 클래스를 분리하여, 입력 및 UI와 데이터를 분리하였음.

## 사용 에셋 
- [AQUAS Lite - Built-In Render Pipeline](https://assetstore.unity.com/packages/vfx/shaders/aquas-lite-built-in-render-pipeline-53519)
- [Pixel Art Icon Pack - RPG](https://assetstore.unity.com/packages/2d/gui/icons/pixel-art-icon-pack-rpg-158343)
- [Free Meat and Skin Icons](https://assetstore.unity.com/packages/2d/gui/icons/free-meat-and-skin-icons-196219)
- [Fantasy Inventory Icons \[Free\]](https://assetstore.unity.com/packages/2d/gui/icons/fantasy-inventory-icons-free-143805)
- [Stones](https://assetstore.unity.com/packages/3d/props/exterior/stones-40329)
- [Free RPG Icons](https://assetstore.unity.com/packages/2d/gui/icons/free-rpg-icons-111659)
- [FREE - RPG Weapons](https://assetstore.unity.com/packages/3d/props/weapons/free-rpg-weapons-199738)
- [Parks and Nature Pack - Lite](https://assetstore.unity.com/packages/3d/props/parks-and-nature-pack-lite-77362)
- [PBR Animated Dinosaurs](https://assetstore.unity.com/packages/3d/characters/animals/pbr-animated-dinosaurs-256019)
- [POLY - Lite Survival Collection](https://assetstore.unity.com/packages/3d/props/poly-lite-survival-collection-220452)
- [Simple Foods](https://assetstore.unity.com/packages/3d/props/food/simple-foods-207032)
- [Survival Game Tools](https://assetstore.unity.com/packages/3d/props/tools/survival-game-tools-139872)
- [Rocky Hills Environment - Light Pack](https://assetstore.unity.com/packages/3d/environments/landscapes/rocky-hills-environment-light-pack-89939)
- [Toon RTS Units - Demo](https://assetstore.unity.com/packages/3d/characters/toon-rts-units-demo-69687)
- [Yughues Free Bushes](https://assetstore.unity.com/packages/3d/vegetation/plants/yughues-free-bushes-13168)
