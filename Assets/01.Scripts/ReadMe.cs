/*
 05. Animations
 
AncientBoss
AncientBossAnimationController

Attack - 범위 설정해둔 오브젝트 활성화 후 준비 시간이 끝난 후  애니메이션이 재생되고 땅에서 오브젝트들이 올라오고 그 오브젝트에 닿으면(오브젝트에 BoxCollider2D 달아줄 예정) Player의 체력이 단다.
Range Attack - 범위 설정해둔 오브젝트 활성화 후 준비 시간이 끝난 후  애니메이션이 재생되고, 적이 바라 보고 있는 방향 아래 조그마한 범위가 나오고 그 범위에 닿으면(Box 오버랩) Player의 체력이 단다.
Burst - 범위 설정해둔 오브젝트 활성화 후 준비 시간이 끝난 후  애니메이션이 재생되고, 적이 바라 보고 있는 방향 아래 조그마한 범위가 나오고 그 범위에 닿으면(Box 오버랩) Player의 체력이 단다. 애니메이션 실행 후 딜 타임이 주어진다.
Buff - 범위 설정해둔 오브젝트 활성화 후 준비 시간이 끝난 후 애니메이션이 발동되고 범위가 켜진다. 범위 내에 있으면 (범위의 중심은 AncientBoss의 중심점이고 범위는 원이다.) Player의 체력이 단다.
Spin Attack - 애니메이션이 실행되면서  범위 설정해둔 오브젝트 활성화 후 Player 위쪽을 따라 다닌다. 준비 시간이 끝나면 아래로 떨어지면서 낙하 공격을 하고 (Box 오버랩) Player의 체력이 단다. 그 후 Spin End가 재생된다.
Spin End - 애니메이션 실행 후 잠깐의 딜 타임이 주어진다.
공격 데미지는 Attack < Range Attack < Burst < Spin Attack<  Buff  - 확률은 반대이다.
Death - 체력이 다 달면 실행되고, BoxCollider2D 가 꺼지고 색은 옅어진다.
Idle - 공격 중이 아닐 때 실행된다. 딜타임이 주어질 때도 실행된다.
Move - 공격 중이 아닐 때 플레이어가 너무 멀어지면 실행된다. 플레이어를 따라가게 한다.
Turnaround - 공격 중이 아닐 때 플레이어의 방향을 보고 있게한다.
Wake - 보스전이 시작될 때 실행된다. 

Enemy
Player의 어그로 중일 때는 땅에 끝에 닿아도 멈추거나 돌지 않고 
그대로 쫒아간다. 어그로는 카메라에서 사라지면 풀리게 된다/

Caged Shocker - Enemy
CagedShockerAnimationController

Attack - 약간의 딜레이 후 애니메이션 실행하고 (Box 오버랩) 범위에 들어온 Player를 공격한다.(공격 전 Idle 상태에 들어간다.)
Death - 체력이 다 달면 실행되고 죽는다.(골드를 준다.)
Hit - 공격 중이 아닐 때 맞으면 실행된다.
Idle - 공격 전 딜레이, 땅에 끝에 닿아 뒤를 돌 때 약간의 딜레이가 있을 예정인데 그때도 실행된다.
Run - 평소 움직일 때 실행된다.

Caged Spider - Enemy
CagedSpiderAnimationController

Death - 체력이 다 달면 실행되고 죽는다.(골드를 준다.)
Move -  플레이어를 발견하고 움직일 때 실행된다.
Sleep - 평소엔 이 애니메이션을 실행하고 있고 Player가 다가가면 Wake가 실행된다.(Player가 일정 범위 이상 멀어지면 다시 실행한다.
Touch Target - 공격 애니메이션이고 공격 전 딜레이가 있다. 범위에 Player가 들어오면  실행하고, 딜레이 중 아무 애니메이션 실행도 된지 않고 움직이지 않는다. 딜레이가 끝나면 애니메이션이 실행되고,  (Box 오버랩) 범위에 들어온 PLayer는 체력이 단다.
Wake -Sleep이 실행 중이고  Player가 다가오면 실행된다.
공격 딜레이 중, 땅의 끝에 닿았을 때 돌기 약간의 딜레이가 있는데 그때도 아무것도 실행되지 않는다.

The Dark Warden - Enemy
TheDarkWardenAnimationController

Attack - 공격 딜레이가 매우 짧고 데미지가 높다. 딜레이 중 Idle 이 실행되고, 딜레이가 끝나면 애니메이션이 실행된다. 딜레이 중 움직이지 않고, 공격 범위에 들어온 Player는 체력이 단다.
Hit - Death 와 같다. 이 적의 체력은 1이다.
Idle - 공격 딜레이 중, 땅의 끝에 닿았을 때 돌기 약간의 딜레이가 있는데 그때도 실행된다.
Run - 평소 움직일 때나 Player를 추격할 때 발동한다. Player 추격 시 속도가 1.5배 빨라진다.

Player
PlayerAnimationController

Idle - 움직이지 않을 때 실행된다.
Walk - 움직일 때 실행된다.
Run Fast - shift 키를 누르고 움직일 때 실행된다. 이때 이동속도가 증가한다.스태미나가 소비된다.
Slash 1 - 콤보 공격1 범위 안에 들어온 모든 Enemy은 체력이 단다.(Box 오버랩), 일정 시간 공격하지 않으면 다음 공격이 Slam으로 전환된다.
Slash 2 - 콤보 공격2 범위 안에 들어온 모든 Enemy은 체력이 단다.(Box 오버랩), 일정 시간 공격하지 않으면 다음 공격이 Slam으로 전환된다.
Spin Attack - 콤보 공격3 범위 안에 들어온 모든 Enemy은 체력이 단다.(Box 오버랩), 일정 시간 공격하지 않으면 다음 공격이 Slam으로 전환된다.
Slam - 콤보 공격4 범위 안에 들어온 모든 Enemy은 체력이 단다.(Box 오버랩), 일정 시간 공격하지 않으면 다음 공격이 Slam으로 전환된다.
Dash - 앞으로 따르게 돌진하면서 실행된다.스태미나가 소비된다.
Roll - 구르기 시 공격을 맞지 않는 상태가 된다.스태미나가 소비된다.
Roll Attack - 구르기 중일 때 공격 하면 실행된다. (Box 오버랩) 공격 범위에 들어온 모든 Enemy들이 모두 체력이 단다.
Jump - 점프할 떄 발동되고, Ledge grab 상태일 때 점프해도 실행된다.스태미나가 소비된다.
Trans - 아래로 추락할 때 Fall 이 발동되기 전에 먼저 실행되고 Fall이 실행된다.
Fall - Trans 실행 후 실행된다.
Fall Attack - Fall 실행 중 공격하면 실행된다. (Box 오버랩) 공격 범위에 들어온 모든 Enemy들이 모두 체력이 단다. 만약 공격이 끝났음에도 떨어지는 중이면 Fall 을 실행하고 한번 바닥에 착지하고 나서 다시 Fall Attack을 사용할 수 있게 된다.
Crouch Land - Fall 상태가 끝나면 발동된다.
Block - 공격을 막는다. Enemy의 공격 타이밍에 잘 맞추면 패링이 되면공격한 Enemy의 체력이 단다.(보스의 공격은 완벽히 막히지 않고, 보스의 공격 중 패링에 성공하면 완벽히 데미지를 막아준다.)
Hit - 아무런 애니메이션이 실행 중이지 않거나, 공격 애니메이션이 실행 중일 때 데미지를 받으면 즉시 타 애니메이션 중지 후 이 애니메이션 발동 후 체력이 단다.
Death - 체력이 다 달면 실행된다. 그 순간 타이틀 화면이 나온다.
Ledge Grab - 벽의 거의 끝자락에서 잡고 있으면 실행된다. - 이 때 점프하면 위로 올라갈 수 있다.
Wall Hold - 벽을 잡고 움직이지 않을 때 실행된다.
Wall Slide - 벽을 잡고 있을 때 s 키나 a 키를 누르면 내려가 진다.
Wall Slide Stop - Wall Slide 이 실행되고  있을 때 움직이지 않으면 실행된다.
Wall Transition - 벽을 잡고 있을 때 Wall Slide가 진행되기 전에 실행시키고 그 다음 Wall Slide를 실행시킨다.


특정 오브젝트에 상호작용(F키) 하면 이단 점프 해금됨
플레이어 공격은 Slash1 < Fall Attack < Slash2 < Roll Attack < Spin Attack < Slam








*/